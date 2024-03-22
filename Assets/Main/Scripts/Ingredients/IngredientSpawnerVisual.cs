using EasyButtons;
using UnityEditor;
using UnityEngine;

namespace Main.Scripts.Ingredients
{
    [RequireComponent(typeof(IngredientSpawner))]
    public class IngredientSpawnerVisual : MonoBehaviour
    {
        [SerializeField] private Transform _visualPart;
        [SerializeField] private ParticleSystem _orbEffect;
        [SerializeField] private IngredientsType _ingredientsType;

#if UNITY_EDITOR

        [SerializeField, HideInInspector] private IngredientsSetup _editor_setup;

        [Button]
        private void Setup()
        {
            if (_visualPart.childCount>0)
            {
                DestroyImmediate(_visualPart.GetChild(0).gameObject);
            }
            var data = _editor_setup.Ingredients.Find(i => i.Type == _ingredientsType);
            Instantiate(data.Ingredient.VisualPart, transform.position, Quaternion.identity, _visualPart);
            transform.name = "Ingredient_Spawner_" + data.Ingredient.name;
            EditorUtility.SetDirty(this);
        }
        
        private void OnValidate()
        {
            if (_editor_setup==null)
            {
                var assets = AssetDatabase.FindAssets("t:ScriptableObject");
            
                foreach (var item in assets)
                {
                    var path = AssetDatabase.GUIDToAssetPath(item);
                    var setup = AssetDatabase.LoadAssetAtPath<IngredientsSetup>(path);
                    if (setup!=null)
                    {
                        _editor_setup = setup;
                        break;
                    }
                }
            }
            
            if (_orbEffect)
            {
                var mainModule = _orbEffect.main;
                var alpha = mainModule.startColor.color.a;

                var data = _editor_setup.Ingredients.Find(i => i.Type == _ingredientsType);
                if (data==null)
                {
                    return;
                }
                var relatedColor = data.RelatedColor;
                relatedColor.a = alpha;

                mainModule.startColor = relatedColor;

            }
        }
        
#endif
        
    }
}

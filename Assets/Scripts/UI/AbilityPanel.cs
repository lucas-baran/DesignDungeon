using UnityEngine;

public sealed class AbilityPanel : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private EAbilityCategory[] _categoryOrder = null;
    [SerializeField] private AbilityDisplay _abilityDisplayPrefab = null;

    private AbilityDisplay[] _abilityDisplayList = null;

    // -- UNITY

    private void Awake()
    {
        _abilityDisplayList = new AbilityDisplay[ _categoryOrder.Length];

        for( int category_index = 0; category_index < _categoryOrder.Length; category_index++ )
        {
            var ability_category = _categoryOrder[ category_index ];

            var ability_display = Instantiate( _abilityDisplayPrefab, transform );
            ability_display.AbilityCategory = ability_category;
            _abilityDisplayList[ category_index ] = ability_display;
        }
    }
}

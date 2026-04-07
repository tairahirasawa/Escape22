using UnityEditor;

namespace I2.Loc
{
    public partial class LocalizationEditor
    {
        [MenuItem("Tools/I2 Localization/Translate All")]
        private static void TranslateAll()
        {
            var path = "Assets/Resources/I2Languages.prefab";
            var source = AssetDatabase.LoadAssetAtPath<LanguageSource>(path);

            if (source == null) return;

            foreach (var term in source.mTerms)
            {
                TranslateLanguage(term.Term, term, null, source.SourceData);
            }
        }
    }
}
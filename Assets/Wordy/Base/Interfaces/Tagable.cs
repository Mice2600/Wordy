using Base.Word;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using UnityEngine;
namespace Base
{
    public interface Tagable
    {
        public List<string> Tags { get; set; }
        public bool IsAnyThereTag() => !Tags.IsEnpty();
        public bool IsThereTag(string NeedTag) => !string.IsNullOrEmpty(Tags.Find(F => F == NeedTag));
        public bool RemoveTag(string Tag) { AllTags = null; return Tags.Remove(Tag); }
        public void AddTag(string Tag) { if (Tags.Contains(Tag)) return; Tags.Add(Tag);AllTags = null;}





        private static Dictionary<string, List<string>> AllTags 
        {
            get 
            {
                if (_AllTags == null) 
                {
                    _AllTags = new Dictionary<string, List<string>>();
                    if (WordBase.Wordgs.Count > 0 && (WordBase.Wordgs[0] is Tagable))
                    {


                        for (int i = 0; i < WordBase.Wordgs.Count; i++)
                        {
                            foreach (var NTag in (WordBase.Wordgs[i] as Tagable).Tags)
                            {
                                if (!AllTags.ContainsKey(NTag))
                                    AllTags.Add(NTag, new List<string>());
                                AllTags[NTag].Add(WordBase.Wordgs[i].EnglishSource);
                            }
                        }
                    }
                }
                return _AllTags;
            }
            set => _AllTags = value;
        }
        private static Dictionary<string, List<string>> _AllTags;
        public static List<string> GetListOfTags() => AllTags.Keys.ToList();
        public static void DestroyTag(string NewID)
        {
            if (WordBase.Wordgs.Count > 0 && (WordBase.Wordgs[0] is Tagable))
                for (int i = 0; i < WordBase.Wordgs.Count; i++)
                    (WordBase.Wordgs[i] as Tagable).RemoveTag(NewID);
            AllTags = null;
            
        }
        public static void AddContent(string TagID, Content Content)
        {
            if (Content is Tagable) (Content as Tagable).AddTag(TagID);
            AllTags = null;
        }
        public static void RemoveContent(string TagID, Content Content)
        {
            if (Content is Tagable) (Content as Tagable).RemoveTag(TagID);
            AllTags = null;
        }

    }
}
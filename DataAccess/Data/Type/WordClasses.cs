using System.Collections.Generic;

namespace iread_interaction_ms.DataAccess.Data.Type{


    class  WordClasses
    {
            public static List<WordClass> elements = new List<WordClass>(){WordClass.ADJECTIVE,
                                                                WordClass.ADVERB,
                                                                WordClass.CONJUNCTION,
                                                                WordClass.NOUN,
                                                                WordClass.PREPOSITION,
                                                                WordClass.PRONOUN,
                                                                WordClass.VERB};
            public static List<string> elementsAsStr = new List<string>(){WordClass.ADJECTIVE.ToString(),
                                                                WordClass.ADVERB.ToString(),
                                                                WordClass.CONJUNCTION.ToString(),
                                                                WordClass.NOUN.ToString(),
                                                                WordClass.PREPOSITION.ToString(),
                                                                WordClass.PRONOUN.ToString(),
                                                                WordClass.VERB.ToString()};
                                                                

    }
}
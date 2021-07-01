export class Tokenizer {

    constructor(){}

    // List of commonly used words that have little meaning to be excluded from analysis.
    stopwords = [
        'about', 'after', 'all', 'also', 'am', 'an', 'and', 'another', 'any', 'are', 'as', 'at', 'be',
        'because', 'been', 'before', 'being', 'between', 'both', 'but', 'by', 'came', 'can',
        'come', 'could', 'did', 'do', 'each', 'for', 'from', 'get', 'got', 'has', 'had',
        'he', 'have', 'her', 'here', 'him', 'himself', 'his', 'how', 'if', 'in', 'into',
        'is', 'it', 'like', 'make', 'many', 'me', 'might', 'more', 'most', 'much', 'must',
        'my', 'never', 'now', 'of', 'on', 'only', 'or', 'other', 'our', 'out', 'over',
        'said', 'same', 'see', 'should', 'since', 'some', 'still', 'such', 'take', 'than',
        'that', 'the', 'their', 'them', 'then', 'there', 'these', 'they', 'this', 'those',
        'through', 'to', 'too', 'under', 'up', 'very', 'was', 'way', 'we', 'well', 'were',
        'what', 'where', 'which', 'while', 'who', 'with', 'would', 'you', 'your',
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
        'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '$', '1',
        '2', '3', '4', '5', '6', '7', '8', '9', '0', '_'
    ];

    trim(array) {
      while (array[array.length - 1] === '')
        array.pop();

      while (array[0] === '')
        array.shift();

      return array;
    };

    tokenize(text) {
      // Break a string up into an array of tokens by anything non-word
      return this.trim(text.split(/\W+/));
    };

}

export class Stemmer extends Tokenizer {

    constructor(){
        super();
    }

    // Denote groups of consecutive consonants with a C and consecutive vowels
    // with a V.
    categorizeGroups(token) {
        return token.replace(/[^aeiou]+/g, 'C').replace(/[aeiouy]+/g, 'V');
    }

    // Denote single consonants with a C and single vowels with a V
    categorizeChars(token) {
        return token.replace(/[^aeiou]/g, 'C').replace(/[aeiouy]/g, 'V');
    }

    // Calculate the "measure" M of a word. M is the count of VC sequences dropping
    // an initial C if it exists and a trailing V if it exists.
    measure(token) {
        if (!token)
        return -1;

        return this.categorizeGroups(token).replace(/^C/, '').replace(/V$/, '').length / 2;
    }

    // Determine if a token end with a double consonant i.e. happ
    endsWithDoublCons(token) {
        return token.match(/([^aeiou])\1$/);
    }

    // Replace a pattern in a word. if a replacement occurs an optional callback
    // can be called to post-process the result. if no match is made NULL is
    // returned.
    attemptReplace(token, pattern, replacement, callback) {
        let result = null;

        if ((typeof pattern == 'string') && token.substr(0 - pattern.length) == pattern)
          result = token.replace(new RegExp(pattern + '$'), replacement);
        else if ((pattern instanceof RegExp) && token.match(pattern))
          result = token.replace(pattern, replacement);

        if (result && callback)
          return callback(result);
        else
          return result;
      }

    // Attempt to replace a list of patterns/replacements on a token for a minimum
    // measure M.
    attemptReplacePatterns(token, replacements, measureThreshold) {
        let replacement = null;

        for (let i = 0; i < replacements.length; i++) {
          if (!measureThreshold || this.measure(this.attemptReplace(token, replacements[i][0], '', '')) > measureThreshold)
            replacement = this.attemptReplace(token, replacements[i][0], replacements[i][1], '');

          if (replacement)
            break;
        }

        return replacement;
      }

    // Replace a list of patterns/replacements on a word. if no match is made return
    // the original token.
    replacePatterns(token, replacements, measureThreshold) {
        const result = this.attemptReplacePatterns(token, replacements, measureThreshold);
        token = !result ? token : result;
        return token;
    }

    // Step 1a as defined for the porter stemmer algorithm.
    step1a(token) {
        if (token.match(/(ss|i)es$/))
        return token.replace(/(ss|i)es$/, '$1');

        if (token.substr(-1) == 's' && token.substr(-2, 1) != 's' && token.length > 3)
        return token.replace(/s?$/, '');

        return token;
    }

    // Step 1b as defined for the porter stemmer algorithm.
    step1b(token) {
        if (token.substr(-3) == 'eed') {
        if (this.measure(token.substr(0, token.length - 3)) > 0)
            return token.replace(/eed$/, 'ee');
        } else {
            const $ref = this;
        const result = this.attemptReplace(token, /ed|ing$/, '', function (token) {
            if ($ref.categorizeGroups(token).indexOf('V') >= 0) {
            const result = $ref.attemptReplacePatterns(token, [['at', 'ate'], ['bl', 'ble'], ['iz', 'ize']], '');
            if (result)
                return result;
            else {
                if ($ref.endsWithDoublCons(token) && token.match(/[^lsz]$/))
                return token.replace(/([^aeiou])\1$/, '$1');

                if ($ref.measure(token) === 1 && $ref.categorizeChars(token).substr(-3) === 'CVC' && token.match(/[^wxy]$/))
                return token + 'e';
            }

            return token;
            }

            return null;
        });

        if (result)
            return result;
        }

        return token;
    }


    // Step 1c as defined for the porter stemmer algorithm.
    step1c(token) {
        // if(categorizeGroups(token).substr(-2, 1) == 'V') {
        //   if(token.substr(-1) == 'y')
        //     return token.replace(/y$/, 'i');
        // }

        return token;
    }

    // Step 2 as defined for the porter stemmer algorithm.
    step2(token) {
        return this.replacePatterns(token, [['ational', 'ate'], ['tional', 'tion'], ['enci', 'ence'], ['anci', 'ance'],
        ['izer', 'ize'], ['abli', 'able'], ['alli', 'al'], ['entli', 'ent'], ['eli', 'e'],
        ['ousli', 'ous'], ['ization', 'ize'], ['ation', 'ate'], ['ator', 'ate'], ['alism', 'al'],
        ['iveness', 'ive'], ['fulness', 'ful'], ['ousness', 'ous'], ['aliti', 'al'],
        ['iviti', 'ive'], ['biliti', 'ble']], 0);
    }

    // Step 3 as defined for the porter stemmer algorithm.
    step3(token) {
        return this.replacePatterns(token, [['icate', 'ic'], ['ative', ''], ['alize', 'al'],
        ['iciti', 'ic'], ['ical', 'ic'], ['ful', ''], ['ness', '']], 0);
    }

    // Step 4 as defined for the porter stemmer algorithm.
    step4(token) {
        return this.replacePatterns(token, [['al', ''], ['ance', ''], ['ence', ''], ['er', ''],
        ['ic', ''], ['able', ''], ['ible', ''], ['ant', ''],
        ['ement', ''], ['ment', ''], ['ent', ''], [/([st])ion/, '$1'], ['ou', ''], ['ism', ''],
        ['ate', ''], ['iti', ''], ['ous', ''], ['ive', ''],
        ['ize', '']], 1);
    }

    // Step 5a as defined for the porter stemmer algorithm.
    step5a(token) {
        // var m = measure(token);

        // if(token.length > 3 && ((m > 1 && token.substr(-1) == 'e') || (m == 1 && !(categorizeChars(token).substr(-4, 3) == 'CVC' && token.match(/[^wxy].$/)))))
        //   return token.replace(/e$/, '');

        return token;
    }

    // Step 5b as defined for the porter stemmer algorithm.
    step5b(token) {
        if (this.measure(token) > 1) {
        if (this.endsWithDoublCons(token) && token.substr(-2) === 'll') {
            return token.replace(/ll$/, 'l');
        }
        }

        return token;
    }



    // perform full stemming algorithm on a single word
    stem(token) {
        return this.step5b(this.step5a(this.step4(this.step3(this.step2(this.step1c(this.step1b(this.step1a(token.toLowerCase())))))))).toString();
    }

    addStopWord(stopWord) {
        this.stopwords.push(stopWord);
    }

    addStopWords(moreStopWords) {
        this.stopwords = this.stopwords.concat(moreStopWords);
    }

    tokenizeAndStem(text, keepStops) {
        const stemmedTokens = [];

        const $ref = this;
        new Tokenizer().tokenize(text).forEach(function(token) {
        if (keepStops || $ref.stopwords.indexOf(token) === -1) {
            stemmedTokens.push($ref.stem(token));
        }
        }.bind(this));

        return stemmedTokens;
    }

}


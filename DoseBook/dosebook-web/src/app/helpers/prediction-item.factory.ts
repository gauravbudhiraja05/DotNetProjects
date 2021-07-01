import { PredictionItem } from '../models/prediction-item.model';

const itemFactory = {};

export class PredictionItemFactory {

    public static createItem(word: string, rank: number): PredictionItem {
        if (!word) {
            throw new Error('parameter "word" must be specified.');
        }

        const returnObject: PredictionItem = {
            w: word, // the word
            f: 0, // frequency how often the user has used this word
            r: rank // an inital rank of the word -> lower means more common
        };
        return returnObject;
    }

}


export default itemFactory;

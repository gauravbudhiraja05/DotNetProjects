export interface PredictionItem {
    w: string; // the word
    f: number; // frequency how often the user has used this word
    r?: number;
    t?: any;
    fuzzyMatch?: boolean;
}

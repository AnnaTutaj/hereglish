import { Pronunciation } from "./Pronunciation";
export interface SaveWord {
    id?: number;
    name: string;
    meaning: string;
    example: string;
    isLearned: boolean;
    categoryId: number;
    subcategoryId: number;
    partOfSpeechId: number;
    features: number[];
    pronunciation: Pronunciation;
}
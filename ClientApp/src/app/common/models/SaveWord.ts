import { Pronunciation } from "./Pronunciation";
export interface SaveWord {
    id?: number;
    name: string;
    meaning: string;
    definition: string;
    example: string;
    link: string;
    isLearned: boolean;
    categoryId: number;
    subcategoryId: number;
    partOfSpeechId: number;
    features: number[];
    pronunciation: Pronunciation;
}
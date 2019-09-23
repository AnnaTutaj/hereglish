import { Pronunciation } from "./Pronunciation";
import { KeyValuePair } from "./KeyValuePair";
export interface Word {
    id: number;
    name: string;
    meaning: string;
    definition: string;
    example: string;
    createdAt: string;
    updatedAt: string;
    link: string;
    isLearned: boolean;
    category: KeyValuePair;
    subcategory: KeyValuePair;
    partOfSpeech: KeyValuePair;
    features: KeyValuePair[];
    pronunciation: Pronunciation;
}
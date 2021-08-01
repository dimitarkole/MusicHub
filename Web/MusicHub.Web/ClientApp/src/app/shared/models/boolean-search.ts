export interface BooleanSearchInput {
	query: string;
}

export enum BooleanSearchEntity {
	Musics = 1,
	Playlists = 2,
}

export interface BooleanSearchEntryMetadata {
	name: string;
	description: string;
}

export interface TableMetadata {
	name: string;
}

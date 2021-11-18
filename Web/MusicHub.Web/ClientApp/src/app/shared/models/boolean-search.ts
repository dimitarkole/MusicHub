import { PermissionType } from "./permissionType";

export interface BooleanSearchInput {
	query: string;
}

export enum BooleanSearchEntity {
	Musics = 1,
	Playlists = 2,
  Categories = 3,
  Users = 4,
	LikedMusics = 5,
}

export interface BooleanSearchEntryMetadata {
	name: string;
  description: string;
  permission: PermissionType;
}

export interface TableMetadata {
	name: string;
}

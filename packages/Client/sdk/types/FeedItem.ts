import { Entity, IsoDate } from '.'

export const enum FeedItemType {
	Article,
	Podcast,
	Video,
}

export interface FeedItem extends Entity {
	date: IsoDate
	title: string
	type: FeedItemType
	imageUrl: string
	link: string
}
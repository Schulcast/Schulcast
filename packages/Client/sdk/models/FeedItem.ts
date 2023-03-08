import { Entity, model } from 'sdk'

export const enum FeedItemType {
	Article,
	Podcast,
	Video,
}

@model('FeedItem')
export class FeedItem extends Entity {
	type = FeedItemType.Article
	date?: MoDate
	title?: string
	imageUrl?: string
	link?: string
	tags = new Array<string>()
}
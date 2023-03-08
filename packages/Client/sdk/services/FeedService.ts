import { Api, FeedItem } from 'sdk'

export class FeedService {
	static getAll() {
		return Api.get<Array<FeedItem>>(`/feed`)
	}
}
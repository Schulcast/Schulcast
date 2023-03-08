import { component, html, PageComponent, route, state } from '@3mo/model'
import { FeedService, FeedItem } from 'sdk'

@route('/olympiade-2023')
@component('sc-page-olympiade-2023')
export class PageOlympiade2023 extends PageComponent {
	static readonly tag = 'olympiade-2023'

	@state() feed = new Array<FeedItem>()

	protected async initialized() {
		this.feed = await FeedService.getAll()
		this.feed = this.feed.filter(f => f.tags.includes(PageOlympiade2023.tag))
	}

	protected get template() {
		return html`
			<mo-page heading='Olympiade 2023'>
				<mo-grid columns='repeat(auto-fill, minmax(250px, 1fr))' gap='1em'>
					${this.feed.map(feedItem => html`<sc-card-feed-item .feedItem=${feedItem}></sc-card-feed-item>`)}
				</mo-grid>
			</mo-page>
		`
	}
}
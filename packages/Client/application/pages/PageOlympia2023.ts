import { component, html, PageComponent, route, state } from '@3mo/model'
import { FeedService, FeedItem } from 'sdk'

@route('/olympia-2023')
@component('sc-page-olympia-2023')
export class PageOlympia2023 extends PageComponent {
	static readonly tag = 'olympia-2023'

	@state() feed = new Array<FeedItem>()

	protected async initialized() {
		this.feed = await FeedService.getAll()
		this.feed = this.feed.filter(f => f.tags.includes(PageOlympia2023.tag))
	}

	protected get template() {
		return html`
			<mo-page heading='Olympia 2023'>
				<mo-grid columns='repeat(auto-fill, minmax(250px, 1fr))' gap='1em'>
					${this.feed.map(feedItem => html`<sc-card-feed-item .feedItem=${feedItem}></sc-card-feed-item>`)}
				</mo-grid>
			</mo-page>
		`
	}
}
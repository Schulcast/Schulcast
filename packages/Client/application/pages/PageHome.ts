
import { component, css, html, nothing, PageComponent, property, route, style } from '@3mo/model'
import { API, FeedItem, Slide } from 'sdk'

@route('/home', '/')
@component('sc-page-home')
export class PageHome extends PageComponent {
	@property({ type: Object }) feed = new Array<FeedItem>()
	@property({ type: Object }) slides = new Array<Slide>()

	protected initialized() {
		this.fetchFeed()
		this.fetchSlides()
	}

	private readonly fetchSlides = async () => this.slides = await API.get('slide') ?? []
	private readonly fetchFeed = async () => this.feed = await API.get('feed') ?? []

	static get styles() {
		return css`
			h2 {
				font: 400 24px/1.3333333333 Roboto;
				text-transform: uppercase;
				margin-top: 32px;
				margin-bottom: 16px;
				text-align: center;
			}

			lit-slider {
				height: 500px;
				--lit-slider-navigation-color: var(--mo-color-accent);
				--lit-slider-theme-color: var(--mo-color-accent);
			}

			lit-slide div {
				font-size: var(--mo-font-size-xl);
				text-align: center;
				text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
			}
		`
	}

	protected get template() {
		return html`
			<lit-page heading='Startseite' fullHeight>
				<mo-flex>
					${!this.slides.length ? nothing : html`
						<lit-slider>
							${this.slides.map(slide => html`
								<lit-slide ${style({ background: `url("http://api.schulcast.de/file/${slide.id}")` })}>
									<div>${slide.description}</div>
								</lit-slide>
							`)}
						</lit-slider>
					`}
					<mo-flex>
						<h2>Zuletzt veröffentlicht</h2>
						<mo-grid columns='repeat(auto-fit, minmax(250px, 1fr))' gap='1em'>
							${this.feed.map(feedItem => html`<sc-card-feed-item .feedItem=${feedItem}></sc-card-feed-item>`)}
						</mo-grid>
					</mo-flex>
				</mo-flex>
			</lit-page>
		`
	}
}
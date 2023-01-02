import { Component, component, css, html, nothing, property, style } from '@3mo/model'
import { FeedItem, FeedItemType } from 'sdk'
import { Player } from '.'
import { PageBlog } from '../pages'

@component('sc-card-feed-item')
export class CardFeedItem extends Component {
	@property({ type: Object }) feedItem!: FeedItem

	static get styles() {
		return css`
			:host {
				height: 100%;
				width: 100%;
				cursor: pointer;
			}

			mo-card {
				height: 100%;
				width: 100%;
				--mo-card-body-padding: 8px 8px 16px 8px;
			}

			mo-icon-button {
				font-size: 56px !important;
			}

			mo-flex[part=image] {
				width: 100%;
				padding-top: 100%;
				background-position: center !important;
				background-repeat: no-repeat !important;
				background-size: cover !important;
				position: relative;
			}

			mo-flex[part=overlay] {
				transition: 250ms;
				opacity: 0;
				position: absolute;
				width: 100%;
				height: 100%;
				overflow: hidden;
				top: 0;
				left: 0;
				right: 0;
				bottom: 0;
				backdrop-filter: blur(20px);
				color: var(--mo-color-accent);
			}

			:host(:hover) mo-flex[part=overlay] {
				opacity: 1;
			}

			span[part=meta] {
				min-width: 0;
				flex: 1;
				color: rgb(128,128,128);
				margin: 0;
				font-size: .85em;
				line-height: 25px;
			}

			span[part=tag] {
				background-color: rgba(var(--subject-tag-background-base), 0.25);
				color: rgb(var(--subject-tag-background-base));
				padding: 0 8px;
				text-transform: uppercase;
				font-size: 0.9em;
				display: flex;
				justify-content: center;
				align-items: center;
				border-radius: 4px;
				width: auto;
			}

			mo-heading {
				min-width: 0;
				text-overflow: ellipsis;
				white-space: normal;
				overflow: hidden;
				font-weight: 400;
				text-align: center;
			}
		`
	}

	protected get template() {
		return !this.feedItem ? nothing : html`
			<style>
				:host {
					--subject-tag-background-base: ${this.subjectTagBackgroundBase};
				}
			</style>
			<mo-card @click=${this.handleClick}>
				<mo-flex slot='media' part='image' ${style({ background: `url("${this.feedItem.type === FeedItemType.Article ? '/assets/blog.jpg' : this.feedItem.imageUrl}")` })}>
					<mo-flex part='overlay' alignItems='center' justifyContent='center'>
						<mo-icon-button icon=${this.feedItem.type === FeedItemType.Article ? 'menu_book' : 'play_arrow'}></mo-icon-button>
						<b>${this.actionText}</b>
					</mo-flex>
				</mo-flex>

				<mo-flex gap='12px' ${style({ width: '100%' })}>
					<mo-flex direction='horizontal' justifyContent='space-between'>
						<span part='meta'>${new MoDate(this.feedItem.date).since().toString()}</span>
						<span part='tag'>${this.subjectTag}</span>
					</mo-flex>

					<mo-heading typography='heading5' part='heading'>${this.feedItem.title}</mo-heading>
				</mo-flex>
			</mo-card>
		`
	}

	private get subjectTag() {
		switch (this.feedItem.type) {
			case FeedItemType.Article:
				return 'Artikel'
			case FeedItemType.Podcast:
				return 'Podcast'
			case FeedItemType.Video:
				return 'Video'
		}
	}

	private get actionText() {
		switch (this.feedItem.type) {
			case FeedItemType.Article:
				return 'Lesen'
			case FeedItemType.Podcast:
				return 'Abspielen'
			case FeedItemType.Video:
				return 'Auf YouTube anschauen'
		}
	}

	private get subjectTagBackgroundBase() {
		switch (this.feedItem.type) {
			case FeedItemType.Article:
				return '111, 116, 221'
			case FeedItemType.Podcast:
				return '0, 137, 123'
			case FeedItemType.Video:
				return '171, 0, 13'
		}
	}


	private readonly handleClick = () => {
		if (!this.feedItem.link) {
			return
		}

		switch (this.feedItem.type) {
			case FeedItemType.Article:
				new PageBlog({ id: Number(this.feedItem.link.split('/')[1]) }).navigate()
				break
			case FeedItemType.Podcast:
				Player.play(this.feedItem.link, this.feedItem.title)
				break
			case FeedItemType.Video:
				window.open(this.feedItem.link, '_blank')
				break
		}
	}
}

declare global {
	interface HTMLElementTagNameMap {
		'sc-card-feed-item': CardFeedItem
	}
}
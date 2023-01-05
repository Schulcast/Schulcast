import { component, Component, css, property } from '@3mo/model'

/** @attr fileId */
@component('sc-thumbnail')
export class Thumbnail extends Component {
	@property({ type: Number }) fileId = 0

	static get styles() {
		return css`
			:host {
				--thumbnail-size: 150px;
				border-radius: 50%;
				width: var(--thumbnail-size);
				height: var(--thumbnail-size);
				margin: 0 15px;
				background-size: cover;
				background-color: var(--mo-color-gray-transparent);
				background-image: var(--sc-thumbnail-background-image);
			}
		`
	}

	protected get template() {
		this.style.setProperty('--sc-thumbnail-background-image', this.fileId > 0 ? `url("/api/files/${this.fileId}")` : '')
		return super.template
	}
}

declare global {
	interface HTMLElementTagNameMap {
		'sc-thumbnail': Thumbnail
	}
}
import { Component, component, css, html } from '@3mo/model'
import { PageImprint } from '../pages'

@component('sc-footer')
export class Footer extends Component {
	static get styles() {
		return css`
			:host {
				position: fixed;
				background: var(--mo-color-background);
				width: 100%;
				border-top: 1px solid var(--mo-color-gray-transparent);
				height: 40px;
				bottom: 0;
				right: 0;
				left: 0;
			}

			img {
				height: 25px;
			}

			a {
				cursor: pointer;
			}

			mo-flex {
				padding: 0 5px;
				width: calc(100% - 10px);
				height: 100%;
			}
		`
	}

	protected get template() {
		return html`
			<mo-flex direction='horizontal' alignItems='center' justifyContent='space-between'>
				<div>
					<a @click=${() => new PageImprint().navigate()}>Impressum</a>
				</div>

				<sc-copyright></sc-copyright>

				<sc-social-media></sc-social-media>
			</mo-flex>
		`
	}
}

declare global {
	interface HTMLElementTagNameMap {
		'sc-footer': Footer
	}
}
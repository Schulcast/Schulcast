import { component, Component, html } from '@3mo/model'

@component('sc-copyright')
export class Copyright extends Component {
	protected get template() {
		return html`
			<mo-flex direction='horizontal' alignItems='center' justifyContent='center' gap='var(--mo-thickness-l)'>
				<a href='https://beruflicheschule-stpauli.hamburg.de/' target='_blank'>
					<img height='30px' src='/assets/schule.png' />
				</a>
				<a>&copy; ${new Date().getFullYear()} Schulcast</a>
			</mo-flex>
		`
	}
}

declare global {
	interface HTMLElementTagNameMap {
		'sc-copyright': Copyright
	}
}
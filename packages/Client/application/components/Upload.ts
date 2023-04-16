import { Component, component, css, html, property, query } from '@3mo/model'
import { Api } from 'sdk'

/** @attr folder */
@component('sc-upload')
export class Upload<T = undefined> extends Component {
	@property() folder?: string

	static get styles() {
		return css`
			:host {
				display: none;
			}
		`
	}

	protected get template() {
		return html`<input name='formFile' type='file'>`
	}

	@query('input') private readonly inputElement!: HTMLInputElement

	get files() {
		return this.inputElement.files as FileList
	}

	open() {
		this.inputElement.click()
	}

	upload = async () => {
		if (this.folder && this.inputElement.value !== '') {
			const response = await Api.post(`/files/${this.folder}`, this.files[0])
			this.inputElement.value = ''
			return response as T
		}

		return undefined
	}
}

declare global {
	interface HTMLElementTagNameMap {
		'sc-upload': Upload
	}
}

import { component, html, property, query } from '@3mo/model'
import { entityDialogComponent, EntityDialogComponent } from './EntityDialogComponent'
import { Upload } from '../components'
import { API, Member, Slide } from 'sdk'

@entityDialogComponent('slide')
@component('sc-dialog-slide')
export class DialogSlide extends EntityDialogComponent<Slide> {
	@property({ type: Object }) members = new Array<Member>()

	@query('sc-upload') private readonly uploadElement!: Upload<{ id: number }>

	protected async initialized() {
		this.members = await API.get('member') ?? []
	}

	private get header() {
		return this.entity.id
			? `Slide #${this.entity.id}`
			: 'Neue Slide'
	}

	protected get template() {
		return html`
			<mo-dialog heading=${this.header}>
				<mo-flex gap='var(--mo-thickness-m)'>
					<mo-text-area label='Beschreibung' required
						value=${this.entity.description}
						@change=${(e: CustomEvent<string>) => this.entity.description = e.detail}
					></mo-text-area>

					<mo-button @click=${() => this.uploadElement.open()}>Bild auswählen</mo-button>
				</mo-flex>

				<sc-upload folder='slides'></sc-upload>
			</mo-dialog>
		`
	}

	protected async save() {
		const image = await this.uploadElement.upload()

		if (!image?.id) {
			return
		}

		await super.save()
	}
}
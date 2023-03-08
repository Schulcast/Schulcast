
import { component, html, query, EntityDialogComponent, ifDefined } from '@3mo/model'
import { Slide, SlideService } from 'sdk'
import { Upload } from '../components'

@component('sc-dialog-slide')
export class DialogSlide extends EntityDialogComponent<Slide> {
	protected entity = new Slide
	protected fetch = SlideService.get
	protected delete = SlideService.delete

	@query('sc-upload') private readonly uploadElement!: Upload<{ id: number }>

	private get header() {
		return this.entity.id
			? `Slide #${this.entity.id}`
			: 'Neue Slide'
	}

	protected get template() {
		return html`
			<mo-entity-dialog heading=${this.header}>
				<mo-flex gap='var(--mo-thickness-m)'>
					<mo-text-area label='Beschreibung' required
						value=${ifDefined(this.entity.description)}
						@change=${(e: CustomEvent<string>) => this.entity.description = e.detail}
					></mo-text-area>

					<mo-button @click=${() => this.uploadElement.open()}>Bild auswählen</mo-button>
				</mo-flex>

				<sc-upload folder='slides'></sc-upload>
			</mo-entity-dialog>
		`
	}

	protected async save(slide: Slide) {
		const image = await this.uploadElement.upload()

		if (!image?.id) {
			return
		}

		await SlideService.save(slide)
	}
}
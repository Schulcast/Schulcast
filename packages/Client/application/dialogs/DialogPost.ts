import { html, component, style, EntityDialogComponent } from '@3mo/model'
import { BlogService, Post } from 'sdk'

@component('sc-dialog-post')
export class DialogPost extends EntityDialogComponent<Post> {
	protected entity = { } as Post
	protected fetch = BlogService.get
	protected save = BlogService.save
	protected delete = BlogService.delete

	private get header() {
		return this.entity.id
			? `Blogeintrag #${this.entity.id}`
			: 'Neuer Blogeintrag'
	}

	protected get template() {
		return html`
			<mo-entity-dialog heading=${this.header} size='medium'>
				<mo-flex gap='var(--mo-thickness-m)' ${style({ height: '100%' })}>
					<mo-field-text label='Titel' required
						value=${this.entity.title ?? ''}
						@change=${(e: CustomEvent<string>) => this.entity.title = e.detail}
					></mo-field-text>

					<sc-field-select-member label='Verfasser'
						value=${this.entity.memberId}
						@change=${(e: CustomEvent<number>) => this.entity.memberId = e.detail}
					></sc-field-select-member>

					<mo-field-text label='Tags'
						value=${this.entity.tags?.join(', ') ?? ''}
						@change=${(e: CustomEvent<string>) => this.entity.tags = e.detail.split(',').map(tag => tag.trim())}
					></mo-field-text>

					<mo-text-area label='Inhalt' rows='11' required ${style({ flex: '1' })}
						value=${this.entity.content ?? ''}
						@change=${(e: CustomEvent<string>) => this.entity.content = e.detail}
					></mo-text-area>
				</mo-flex>
			</mo-entity-dialog>
		`
	}
}
import { property, html, component, style } from '@3mo/model'
import { entityDialogComponent, EntityDialogComponent } from './EntityDialogComponent'
import { API, Member, Post } from 'sdk'

@entityDialogComponent('blog')
@component('sc-dialog-post')
export class DialogPost extends EntityDialogComponent<Post> {
	@property({ type: Object }) members = new Array<Member>()

	protected async initialized() {
		this.members = await API.get<Array<Member>>('member') ?? []
	}

	private get header() {
		return this.entity.id
			? `Blogeintrag #${this.entity.id}`
			: 'Neuer Blogeintrag'
	}

	protected get template() {
		return html`
			<mo-dialog heading=${this.header} size='medium'>
				<mo-flex gap='var(--mo-thickness-m)' ${style({ height: '100%' })}>
					<mo-field-text label='Titel' required
						value=${this.entity.title ?? ''}
						@change=${(e: CustomEvent<string>) => this.entity.title = e.detail}
					></mo-field-text>

					<mo-field-select label='Verfasser'
						value=${this.entity.memberId}
						@change=${(e: CustomEvent<number>) => this.entity.memberId = e.detail}
					>
						${this.members.map(member => html`
							<mo-option value=${String(member.id)} ?selected=${member.id === this.entity.memberId}>
								${member.nickname}
							</mo-option>
						`)}
					</mo-field-select>

					<mo-text-area label='Inhalt' rows='11' required ${style({ flex: '1' })}
						value=${this.entity.content ?? ''}
						@change=${(e: CustomEvent<string>) => this.entity.content = e.detail}
					></mo-text-area>
				</mo-flex>
			</mo-dialog>
		`
	}
}

import { component, html, property, query } from '@3mo/model'
import { entityDialogComponent, EntityDialogComponent } from './EntityDialogComponent'
import { API, Member, MemberTask, Task } from 'sdk'
import { Upload } from '../components'
import { sha256 } from 'js-sha256'

@entityDialogComponent('member')
@component('sc-dialog-member')
export class DialogMember extends EntityDialogComponent<Member> {
	@property({ type: Object }) tasks = new Array<Task>()

	@query('sc-upload') private readonly uploadElement?: Upload<{ id: number }>

	protected async initialized() {
		this.tasks = await API.get('task') ?? []
	}

	private get header() {
		return this.entity.id
			? `Mitglied #${this.entity.id}`
			: 'Neuer Mitglied'
	}

	protected get template() {
		return html`
			<mo-dialog heading=${this.header}>
				<mo-flex gap='var(--mo-thickness-l)'>
					<mo-flex alignItems='center' gap='var(--mo-thickness-l)'>
						<sc-thumbnail ?hidden=${!this.entity.imageId} fileId=${this.entity.imageId}></sc-thumbnail>
						<mo-button @click=${() => this.uploadElement?.open()}>${this.uploadButtonLabel}</mo-button>
					</mo-flex>

					<mo-field-text label='Spitzname' required
						value=${this.entity.nickname}
						@change=${(e: CustomEvent<string>) => this.entity.nickname = e.detail}
					></mo-field-text>

					<mo-field-password label='Passwort' required
						value=${this.entity.id ? '[UNVERÄNDERT]' : ''}
						@change=${(e: CustomEvent<string>) => this.entity.password = sha256(e.detail)}
					></mo-field-password>

					<mo-field-select label='Aufgabe' multiple
						.value=${this.entity.tasks?.map(task => String(task.taskId))}
						@change=${(e: CustomEvent<Array<number>>) => this.entity.tasks = e.detail.map(taskId => ({ taskId: taskId, memberId: this.entity.id })) as Array<MemberTask>}
					>
						${this.tasks.map(task => html`
							<mo-option value=${task.id} multiple>${task.title}</mo-option>
						`)}
					</mo-field-select>
				</mo-flex>

				<sc-upload folder='members'></sc-upload>
			</mo-dialog>
		`
	}

	private get uploadButtonLabel() {
		switch (true) {
			case (this.uploadElement?.files.length ?? 0) > 0:
				return `${this.uploadElement?.files.length} Bild ausgewählt`
			case !!this.entity.imageId:
				return 'Bild ersetzen'
			default:
				return 'Bild auswählen'
		}
	}

	protected async save() {
		await this.uploadImageIfSet()
		await super.save()
	}

	private readonly uploadImageIfSet = async () => {
		if (!this.uploadElement || !this.uploadElement.files.length) {
			return
		}

		const image = await this.uploadElement.upload()
		this.entity.imageId = image?.id ?? 0
	}
}
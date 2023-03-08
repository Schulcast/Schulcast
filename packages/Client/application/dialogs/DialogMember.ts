
import { component, html, query, EntityDialogComponent, ifDefined } from '@3mo/model'
import { Member, MemberService, MemberTask } from 'sdk'
import { Upload } from '../components'
import { sha256 } from 'js-sha256'

@component('sc-dialog-member')
export class DialogMember extends EntityDialogComponent<Member> {
	protected entity = new Member
	protected fetch = MemberService.get
	protected delete = MemberService.delete

	@query('sc-upload') private readonly uploadElement?: Upload<{ id: number }>

	private get header() {
		return this.entity.id
			? `Mitglied #${this.entity.id}`
			: 'Neuer Mitglied'
	}

	protected get template() {
		return html`
			<mo-entity-dialog heading=${this.header}>
				<mo-flex gap='var(--mo-thickness-l)'>
					<mo-flex alignItems='center' gap='var(--mo-thickness-l)'>
						<sc-thumbnail ?hidden=${!this.entity.imageId} fileId=${ifDefined(this.entity.imageId)}></sc-thumbnail>
						<mo-button @click=${() => this.uploadElement?.open()}>${this.uploadButtonLabel}</mo-button>
					</mo-flex>

					<mo-field-text label='Spitzname' required
						value=${ifDefined(this.entity.nickname)}
						@change=${(e: CustomEvent<string>) => this.entity.nickname = e.detail}
					></mo-field-text>

					<mo-field-password label='Passwort' required
						value=${this.entity.id ? '[UNVERÄNDERT]' : ''}
						@change=${(e: CustomEvent<string>) => this.entity.password = sha256(e.detail)}
					></mo-field-password>

					<sc-field-select-task label='Aufgabe' multiple
						.value=${this.entity.tasks?.map(task => String(task.taskId))}
						@change=${(e: CustomEvent<Array<number>>) => this.entity.tasks = e.detail.map(taskId => ({ taskId: taskId, memberId: this.entity.id })) as Array<MemberTask>}
					></sc-field-select-task>
				</mo-flex>

				<sc-upload folder='members'></sc-upload>
			</mo-entity-dialog>
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

	protected async save(member: Member) {
		await this.uploadImageIfSet()
		await MemberService.save(member)
	}

	private readonly uploadImageIfSet = async () => {
		if (!this.uploadElement || !this.uploadElement.files.length) {
			return
		}

		const image = await this.uploadElement.upload()
		this.entity.imageId = image?.id ?? 0
	}
}
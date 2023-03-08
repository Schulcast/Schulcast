import { component, html, EntityDialogComponent } from '@3mo/model'
import { Task, TaskService } from 'sdk'

@component('sc-dialog-task')
export class DialogTask extends EntityDialogComponent<Task> {
	protected entity = new Task
	protected fetch = TaskService.get
	protected save = TaskService.save
	protected delete = TaskService.delete

	private get header() {
		return this.entity.id
			? `Aufgabe #${this.entity.id}`
			: 'Neue Aufgabe'
	}

	protected get template() {
		return html`
			<mo-entity-dialog heading=${this.header}>
				<mo-field-text label='Titel' required
					value=${this.entity.title}
					@change=${(e: CustomEvent<string>) => this.entity.title = e.detail}
				></mo-field-text>
			</mo-entity-dialog>
		`
	}
}
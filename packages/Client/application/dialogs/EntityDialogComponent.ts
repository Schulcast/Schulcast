import { DialogComponent, PropertyValues, DialogDefault } from '@3mo/model'
import { Entity, API } from 'sdk'

export const entityDialogComponent = (controller: string) => {
	return (constructor: Constructor<DialogComponent<any, any>>) => {
		(constructor as any).controller = controller
	}
}

export abstract class EntityDialogComponent<T extends Entity> extends DialogComponent<{ entity?: T }> {
	static controller: string

	protected readonly entity = this.parameters.entity ?? {} as T

	private get controller() {
		return (this.constructor as any).controller
	}

	protected firstUpdated(props: PropertyValues) {
		super.firstUpdated(props)
		if (this.dialogElement) {
			(this.dialogElement as any).primaryButtonText = 'Speichern';
			(this.dialogElement as any).secondaryButtonText = this.parameters.entity?.id ? 'Löschen' : '';
		}
	}

	protected primaryAction() {
		this.save()
	}

	protected async save() {
		if (!this.parameters.entity?.id) {
			await API.post(this.controller, this.entity)
		} else {
			await API.put(`${this.controller}/${this.parameters.entity.id}`, this.entity)
		}
	}

	protected secondaryAction() {
		this.delete()
	}

	protected async delete() {
		if (!this.parameters.entity?.id) {
			return
		}

		await new DialogDefault({
			heading: 'Bestätigung',
			content: 'Soll dieser Eintrag gelöscht werden?',
		}).confirm()

		await API.delete(`${this.controller}/${this.parameters.entity.id}`)
	}
}
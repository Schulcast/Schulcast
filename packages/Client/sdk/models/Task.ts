import { Entity, MemberTask, model } from 'sdk'

@model('Task')
export class Task extends Entity {
	title!: string
	members = new Array<MemberTask>()
}
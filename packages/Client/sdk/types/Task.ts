import { Entity, MemberTask } from '.'

export interface Task extends Entity {
	title: string
	members: Array<MemberTask>
}
import { Member, Task, model } from 'sdk'

@model('MemberTask')
export class MemberTask {
	taskId?: number
	task?: Task
	memberId?: number
	member?: Member
}
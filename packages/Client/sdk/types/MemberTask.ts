import { Member, Task } from '.'

export interface MemberTask {
	taskId: number
	task: Task
	memberId: number
	member: Member
}
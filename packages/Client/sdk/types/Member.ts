import { Entity, MemberTask, Post, MemberData } from '.'

export interface Member extends Entity {
	role: 'Admin' | 'Member'
	nickname: string
	password: string
	token: string
	imageId: number
	tasks?: Array<MemberTask>
	data: Array<MemberData>
	posts: Array<Post>
}
import { Entity, MemberTask, Post, MemberData, model } from 'sdk'

@model('Member')
export class Member extends Entity {
	role: 'Admin' | 'Member' = 'Member'
	nickname?: string
	password?: string
	token?: string
	imageId?: number
	tasks = new Array<MemberTask>()
	data = new Array<MemberData>()
	posts = new Array<Post>()
}
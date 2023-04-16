import { Api, Member } from 'sdk'

export class MemberService {
	static get(id: number) {
		return Api.get<Member>(`/member/${id}`)
	}

	static getAuthenticated() {
		return Api.get<Member | undefined>(`/member/authenticated`)
	}

	static getAll() {
		return Api.get<Array<Member>>(`/member`)
	}

	static save(member: Member) {
		return !member.id
			? Api.post<Member>('/member', member)
			: Api.put<Member>(`/member`, member)
	}

	static delete(member: Member) {
		return Api.delete(`/member/${member.id}`)
	}

	static async authenticate(username: string, password: string) {
		const token = await Api.get<string>(`/member/authenticate?nickname=${username}&password=${password}`)
		Api.authenticator?.authenticate(token)
	}

	static async isAuthenticated() {
		// eslint-disable-next-line no-return-await
		return !!Api.authenticator?.isAuthenticated() && await Api.get<boolean>('/member/is-authenticated')
	}

	static unauthenticate() {
		Api.authenticator?.unauthenticate()
	}
}
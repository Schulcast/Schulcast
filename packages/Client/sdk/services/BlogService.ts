import { Api, Post } from 'sdk'

export class BlogService {
	static get(id: number) {
		return Api.get<Post>(`/blog/${id}`)
	}

	static getAll() {
		return Api.get<Array<Post>>(`/blog`)
	}

	static save(post: Post) {
		return !post.id
			? Api.post<Post>('/blog', post)
			: Api.put<Post>(`/blog`, post)
	}

	static delete(post: Post) {
		return Api.delete(`/blog/${post.id}`)
	}
}
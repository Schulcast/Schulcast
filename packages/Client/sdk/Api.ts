// eslint-disable-next-line import/no-internal-modules
import { DialogAuthenticate } from 'application/dialogs'

export class API {
	static get<T = any>(route: string) {
		return this.fetch<T>('GET', route)
	}

	static post<T = any>(route: string, data: unknown) {
		return this.fetch<T>('POST', route, JSON.stringify(data))
	}

	static postFile<T = any>(route: string, fileList: FileList) {
		const form = new FormData()
		form.set('formFile', fileList[0], fileList[0].name)
		return this.fetch<T>('POST', route, form)
	}

	static put<T = any>(route: string, data: unknown) {
		return this.fetch<T>('PUT', route, JSON.stringify(data))
	}

	static delete<T = any>(route: string) {
		return this.fetch<T>('DELETE', route)
	}

	private static async fetch<T = unknown>(method: 'GET' | 'POST' | 'PUT' | 'DELETE', route: string, body: BodyInit | null = null): Promise<T | undefined> {
		const token = DialogAuthenticate.authenticatedMember?.token

		const headers: HeadersInit = {
			// eslint-disable-next-line @typescript-eslint/naming-convention
			'Accept': 'application/json',
			// eslint-disable-next-line @typescript-eslint/naming-convention
			'Authorization': `Bearer ${token}`
		}

		const isForm = body instanceof FormData
		if (isForm === false) {
			headers['Content-Type'] = 'application/json'
		}

		const url = `/api/${route}`
		const response = await fetch(url, {
			method: method,
			credentials: 'omit',
			headers: headers,
			referrer: 'no-referrer',
			body: body
		})

		if (response.status >= 400) {
			throw new Error(`${response.statusText}. ${await response.text()}`)
		}

		if (method === 'DELETE') {
			return undefined
		}

		return await response.json() as T
	}
}
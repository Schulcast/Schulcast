import { component } from '@a11d/lit'
import { authenticator } from '@a11d/lit-application-authentication'
import { BusinessSuiteDialogAuthenticator } from '@3mo/model'
import { MemberService, JwtApiAuthenticator } from 'sdk'

@authenticator()
@component('sc-dialog-authentication')
export class DialogAuthentication extends BusinessSuiteDialogAuthenticator {
	static get isAuthenticated() { return !!JwtApiAuthenticator.token }

	protected async requestAuthentication() {
		await MemberService.authenticate(this.username, this.password)
		return {
			id: 1,
			name: this.username,
			email: '',
		}
	}

	protected requestUnauthentication() {
		return Promise.resolve(MemberService.unauthenticate())
	}

	protected isAuthenticatedServerSide() {
		return MemberService.isAuthenticated()
	}

	// eslint-disable-next-line require-await
	protected async requestPasswordReset() {
		throw new Error('Please contact your administrator to reset your password.')
	}
}
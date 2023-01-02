import { component, css, html, PageComponent, route } from '@3mo/model'

@route('/imprint')
@component('sc-page-imprint')
export class PageImprint extends PageComponent {
	static get styles() {
		return css`
			div {
				line-height: 175%;
			}

			h2 {
				color: var(--mo-accent);
				font-weight: 400;
			}

			a {
				color: var(--mo-accent);
			}
		`
	}

	protected get template() {
		return html`
			<mo-page heading='Impressum'>
				<div>
					<h2>Generell</h2>
					Schulcast.de ist ein publizistisches Angebot von:
					<br />
					dennis ofosu (verantwortlich)
					<br />
					Rombergstraße 25
					<br />
					20255 hamburg
					<br />
					Deutschland
					<br />
					dennis.ofosu@gmail.com
					<br />
					040 4289730

					<h2>Mitarbeit & Open-Source Projekte</h2>
					Entwickelt von <a href='https://github.com/arshia11d'>arshia11d</a> basierend auf <a href='https://github.com/3mo-esolutions/model'>MoDeL</a>-Projekt.
					<br />
					<h2> Urheberrecht, Haftung </h2>
					Das Layout der Homepage, die verwendeten Grafiken sowie die Sammlung der Beiträge sind urheberrechtlich geschützt. Die Seiten dürfen nur zum privaten Gebrauch vervielfältigt, Änderungen nicht vorgenommen und Vervielfältigungsstücke weder verbreitet noch zur öffentlichen Wiedergabe benutzt werden.
					<br />
					Die einzelnen Beiträge sind ebenfalls urheberrechtlich geschützt; weitere Hinweise können ggf. dort nachgelesen werden. Alle Informationen auf diesem Server erfolgen ohne Gewähr für ihre Richtigkeit. In keinem Fall wird für Schäden, die sich aus der Verwendung der abgerufenen Informationen ergeben, eine Haftung übernommen.
					<br />
					Das genehmigte Bildmaterial (Fotos, Grafiken, Videos) stammt in der Regel von der Homepageredaktion und KollegInnen der BS11. Außerredaktionelle und außerschulische Quellen sind explizit gekennzeichnet. Dies gilt uneingeschränkt auch für den RSS-Feed.

					<h2> Datenschutz </h2>

					schulcast.de hält sich an geltende Datenschutzgesetze und bemüht sich um Datensparsamkeit. Vereinzelnd fallen Informationen wie IP-Adressen oder der Browser-Typ an, die ausschließlich intern Verwendung finden – beispielsweise für Statistiken. Keine Nutzer-Daten oder Mailadressen werden an Dritte weitergegeben. Bei der Verwendung von Diensten Dritter gelten dessen Datenschutz- und Nutzungsbedingungen. Für schulcast.de sind dies beispielsweise Twitter und YouTube.
					<br />
					Mehr Informationen: Datenschutzerklärung.

					<h2> Korrekturen </h2>

					Grammatikalische und inhaltliche Fehler passieren. Ich versuche diese verständlich und unverzüglich zu korrigieren.
				</div>
			</mo-page>
		`
	}
}
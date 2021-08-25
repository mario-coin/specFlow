using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using ui.test.Drivers;
using ui.test.Pages;

namespace ui.test.Steps
{
    [Binding]
    class Login : Browser
    {
        private LoginPage loginPage = new LoginPage();

        [Given(@"que acesso o site")]
        public void DadoQueAcessoOSite()
        {
            Browser.loadPage("https://www.saucedemo.com/");
        }

        [Then(@"vejo que estou na login page")]
        public void EntaoVejoQueEstouNaLoginPage()
        {
            Assert.That(loginPage.isBotImgExist);
        }

        [When(@"informo as seguintes credenciais")]
        public void QuandoInformoAsSeguintesCredenciais(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"me autentico no sistema")]
        public void QuandoMeAutenticoNoSistema()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"o menu do usuário está visível")]
        public void EntaoOMenuDoUsuarioEstaVisivel()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"o usuário aparece logado")]
        public void EntaoOUsuarioApareceLogado()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"um erro aparece informando que o usuário está bloqueado")]
        public void EntaoUmErroApareceInformandoQueOUsuarioEstaBloqueado()
        {
            ScenarioContext.Current.Pending();
        }

    }
}

# Shopping API

Este projeto mostra um exemplo de uma API que simula um carrinho de compras online.

## Introdução

Se você é como eu e adora comprar coisas pela Internet, já deve ter se perguntado como funciona o conceito de carrinho de compras online. Geralmente, todas as interações com o carrinho de compras de um usuário podem ser salvas diretamente em um banco de dados online – mas isso pode ser muito arriscado devido a um conjunto de fatores, como falta de segurança, ou menos integração com outras ferramentas (por exemplo, acesso por smartphone). Por isso, as compras online tendem a preferir o uso de uma API. 

Uma **API** é uma interface que atua como uma "ponte" entre uma aplicação e um servidor com os dados solicitados. A aplicação faz uma solicitação à API, que a processa, repassa ao servidor, obtém os dados solicitados, e retorna os dados à aplicação. Esses dados, então, podem ser manipulados pela aplicação da maneira que desejar. 

## Sobre Este Projeto

Este projeto é feito no [**Visual Studio 2022**](https://visualstudio.microsoft.com/vs/). Estruturalmente falando, existem dois objetos principais dentro deste projeto:

- O **código-fonte** (obviamente!);
- A **fonte de dados**, dentro da pasta _Database_, que contém o banco de dados SQL, e os scripts necessários para alimentar o banco de dados antes de executar a aplicação (*DB_TABLE_CREATION* e *DB_TABLE_FEEDING*). 

### Fonte de Dados

Atualmente, a fonte de dados deste projeto é um banco de dados [**SQLite**](https://www.sqlite.org/). Eu preferi o SQLite porque é fácil de configurar e acessar o conteúdo dele, já que é apenas um arquivo comum -- mas não recomendo o SQLite para grandes aplicações, devido à falta de velocidade de acesso, escalabilidade, e segurança. 

### O Conceito de Entidade

Criei um conceito, que chamei de entidade (**Entidade**), para produzir as consultas SQL necessárias para realizar operações no banco de dados. Uma Entidade é definida, neste contexto, como uma representação de um objeto no banco de dados, incluindo suas operações – por exemplo, uma tabela SQL. Mais detalhes de como esse conceito funciona podem ser encontrados no próprio código-fonte. 

## Informações Adicionais

Como isso é um trabalho em progresso, provavelmente adicionarei algumas coisas futuramente (e.g. treatement of corner cases and bug fixes). Fique atento!

## Licença de Uso

Os códigos disponibilizados aqui estão sob a licença Apache, versão 2.0 (veja o arquivo `LICENSE` em anexo para mais detalhes). Dúvidas sobre este projeto podem ser enviadas para o meu e-mail: carloswdecarvalho@outlook.com.
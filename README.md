# WhatHaveYouLost (O que você perdeu) // TechChallenge

Este é o repositório da aplicação WhatHaveYouLost, que tem como objetivo mostrar notícias relevantes que os usuários podem ter perdido enquanto estavam no celular. A aplicação é construída utilizando C# e Razor Pages, e utiliza tecnologias como SQL e Azure para fornecer uma experiência de usuário intuitiva e eficiente.

# Status do Projeto
| Branch | Status |
| ----- | -----|
| Build Azure | [![Build and deploy ASP.Net Core app to Azure Web App - oqueperdi](https://github.com/darakimberlys/WhatHaveYouLost/actions/workflows/master_oqueperdi.yml/badge.svg)](https://github.com/darakimberlys/WhatHaveYouLost/actions/workflows/master_oqueperdi.yml)
| Tests | [![.NET](https://github.com/darakimberlys/WhatHaveYouLost/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/darakimberlys/WhatHaveYouLost/actions/workflows/dotnet.yml)
| Postman Integrated Tests | [![Automated API tests using Postman CLI](https://github.com/darakimberlys/WhatHaveYouLost/actions/workflows/postman_test.yml/badge.svg?branch=master)](https://github.com/darakimberlys/WhatHaveYouLost/actions/workflows/postman_test.yml)

## Funcionalidades

- Acesso à página de notícias, onde os usuários podem encontrar uma lista de títulos de notícias relevantes e interessantes.
- Cada título de notícia é um link para a página de detalhes, onde os usuários podem ler o conteúdo completo da notícia, visualizar a imagem associada e ver a fonte da notícia.

## Tecnologias Utilizadas

- C# e Razor Pages para a interface do usuário.
- SQL para o banco de dados, onde as informações das notícias são armazenadas na tabela NewsData.
- Azure para hospedagem da aplicação.
- Blob Storage para armazenamento e recuperação de imagens associadas às notícias.

## Como Rodar os Testes Integrados

Para rodar os testes integrados, siga os passos abaixo:

1. Acesse [este link](https://www.postman.com/payload-explorer-79364497/workspace/my-workspace/folder/31016440-fd5a71e5-efa6-4340-9997-02286321707c) para abrir a coleção de testes no Postman.
2. Importe a coleção no seu ambiente Postman.
3. Execute os testes para garantir que a aplicação está funcionando corretamente.

## Prévia do site

![Preview do Site](https://github.com/darakimberlys/WhatHaveYouLost/assets/40128511/29b87ac5-bc2f-42ce-841b-6ebf2aaefd9d)
*Esta é uma prévia do [site](https://oqueperdi.azurewebsites.net) mostrando a página inicial.*

## Diagrama Simples

![Diagrama da Aplicação](https://github.com/darakimberlys/WhatHaveYouLost/blob/3c87fe934487d3df2095acc26354f3f2aa27c0b0/Diagrama-WHTL.png)

[Colaboradores](https://github.com/darakimberlys/WhatHaveYouLost/graphs/contributors)

# WhatHaveYouLost (O que você perdeu) // TechChallenge#1

A aplicação em C# e Razor Pages tem como objetivo mostrar algumas das notícias que você pode ter perdido enquanto estava no celular. A aplicação utiliza SQL na tabela NewsData para armazenar as informações das notícias, utiliza Razor para a interface do usuário e está hospedada no Azure. Além disso, as imagens das notícias são obtidas do Blob Storage.

A aplicação permite que os usuários acessem a página de notícias, onde encontrarão uma lista de títulos de notícias que foram selecionadas como relevantes e interessantes. Cada título de notícia é um link para a página de detalhes, onde os usuários podem ler o conteúdo completo da notícia, visualizar a imagem associada e ver a fonte da notícia.

Os dados das notícias são armazenados em uma tabela chamada NewsData no banco de dados SQL. Essa tabela contém os campos relevantes, como título, conteúdo, URL da imagem e fonte da notícia. As informações das notícias são recuperadas do banco de dados e exibidas nas páginas correspondentes.

As imagens das notícias são armazenadas no Blob Storage, um serviço de armazenamento em nuvem oferecido pelo Azure. A aplicação se conecta ao Blob Storage para obter as imagens associadas a cada notícia e as exibe nas páginas correspondentes.

No geral, essa aplicação proporciona aos usuários uma maneira fácil e conveniente de acessar e ler notícias relevantes que possam ter perdido enquanto estavam no celular. Ela utiliza tecnologias modernas, como C#, Razor Pages, SQL e Azure, para fornecer uma experiência de usuário intuitiva e eficiente.

## Diagrama simples:
![image](https://github.com/darakimberlys/WhatYouHaveLost/assets/40128511/482b2416-2f43-4459-9be0-0ecfd327096c)

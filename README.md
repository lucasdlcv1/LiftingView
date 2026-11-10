# 🏋️‍♂️ LiftingView

> **LiftingView** é uma aplicação web desenvolvida para o gerenciamento de treinos de força, acompanhamento de cargas e visualização gráfica de progressão contínua (*Progressive Overload*).

---

## 🎯 Sobre o Projeto

O **LiftingView** surge para resolver a dor de praticantes de musculação e atletas de força que buscam monitorar sua evolução real de forma simples, precisa e visual. 

Ao contrário de fichas em papel ou blocos de notas genéricos, o sistema separa o **planejamento (Ficha de Treino)** do **fato consumado (Registro Executado)**. Isso permite alterar séries, cargas e aplicar **Técnicas Especiais** (como Isometria, Drop-sets e Rest-pause) em tempo real na academia sem violar o histórico nem modificar a estrutura da ficha original.

---

## ✨ Principais Funcionalidades

- **🗂️ Cadastro de Exercícios:** Catálogo organizado por grupos musculares com orientações de execução.
- **📋 Montagem de Fichas de Treino:** Criação de rotinas personalizadas (ABC, Upper/Lower, etc.) com cálculo automático do **Volume Load Estimado**.
- **⏱️ Registro de Treino em Tempo Real:** Execução flexível com suporte a adição/remoção de séries, alteração pontual de cargas e temporizador de descanso.
- **⚡ Volume Load Dinâmico ($VTT$):** Cálculo instantâneo do volume de treino ($\text{Carga} \times \text{Repetições} \times \text{Fator de Técnica}$) atualizado a cada repetição concluída.
- **📈 Gráficos de Progressão:** Acompanhamento visual da evolução de cargas e volume acumulado por exercício e por treino ao longo do tempo.
- **⚙️ Técnicas Avançadas Pré-cadastradas:** Suporte a multiplicadores de intensidade para métricas refinadas de sobrecarga.

---

## 🏗️ Arquitetura e Tecnologias

O projeto adota os princípios de **Domain-Driven Design (DDD)** e **Clean Architecture**, garantindo desacoplamento entre as regras de negócio e os componentes de infraestrutura.

### **Tech Stack**

- **Backend:** C# / .NET Core (ASP.NET Core MVC)
- **Persistence:** Entity Framework Core
- **Database:** SQL Server / PostgreSQL
- **Frontend:** Bootstrap
- **Tooling & Containers:** Docker, VS Code, Git

---

## 📐 Estrutura do Domínio

```text
LiftingView.Dominio/
 ├── Exercicios/          # Entidades e Value Objects do catálogo de exercícios
 ├── Treinos/           # Agregado de Fichas e Planejamento de Treinos
 ├── Registros/        # Agregado de Registros e Execuções de Treinos no dia

# SO-Architecture

## Descrição
Um sistema para arquitetar projetos na Unity baseado em [ScriptableObjects](https://docs.unity3d.com/Manual/class-ScriptableObject.html).

# Features

## SO Generator

Essa janela pode criar *GameEvents*, *Variables* e *Collections* do tipo especificado, no diretório desejado, para acessa-la basta ir em Window > SOGenerator.

![](https://i.imgur.com/Bkyu7Hi.png)

Cada uma das opções a seguir tem como objetivo criar os [ScriptableObjects](https://docs.unity3d.com/Manual/class-ScriptableObject.html) com as características que o desenvolvedor necessitar.

![](https://i.imgur.com/yeAdQM1.png)

O diretório em que os [ScriptableObjects](https://docs.unity3d.com/Manual/class-ScriptableObject.html) serão guardados é gerado altomaticamente, baseado no nome dado a eles no campo *Text file* ou *Text*. 

Supondo que o desenvolvedor criou um [*enum*](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum) chamado *Direction*, como demostrado abaixo:

```csharp
namespace Game
{
    public enum Direction
    {
        Right = 1,
        Left = -1
    }
}
```

Então, ele decide criar os ScriptableObjects de *Direction*, para isso ele marca as opções *Collection* e *Variable*. (como as *Variables* dependem, também, dos *GameEvents* eles são marcados automaticamente).

![](https://i.imgur.com/5kewSTq.png)

Após isso, é necessário escolher quais **Namespaces** o desenvolvedor quer importar nos ScriptableObjects que estão sendo criados.
Como o enum *Direction* pertence à **Namespace** *Game* ela é adicionada na lista de **Namespaces**.

![](https://i.imgur.com/aumozay.png)

Para que o gerador saiba qual nome dar aos arquivos que serão criados, o desenvolvedor pode colocar no campo *Text file* o arquivo *Direction.cs* que ele criou, ao criar o *enum*, ou apenas escrever "Direction" no campo *Text*.

- O botão **Asset** permite que o campo receba um **Text Asset**, que nesse caso é o arquivo que o desenvolvedor criou.
- O botão **String** permite que o campo receba uma **string**, que nesse caso receberia o valor "Direction".

No exemplo o desenvolvedor decide usar a opção **String**.

![](https://i.imgur.com/Az5R8DH.png)

Em seguida é necessário especificar onde os arquivos serão salvos, para isso basta clicar no botão **Select Folder**.

![](https://i.imgur.com/VUqSEQ7.png)

Então, com tudo configurado, os **ScriptableObjects** já podem ser criados, clicando no botão **Create**.

O diretório que será criado possui o nome escrito no campo *Text* ou o nome do arquivo especificado no campo *Text File* seguido por "SO", que denota que naquele directorio os arquivos do tipo escolhido estão sendo guardados.

![](https://i.imgur.com/aw9PjYZ.png)

Uma observação válida é que os **Editors** também são criados.

## Variables

As *Variables* são **ScriptableObjects** que armazenam valores de um unico tipo de dado e, frequentemente, são utilizadas para que diferentes Scripts possam ter acesso aos mesmos dados sem, necessariamente, se refenciarem entre si.

Para criar uma *Variable* basta clicar em Crete > SOArchitecture > Variables e escolher o tipo de variável.

![](https://i.imgur.com/ARWh95cl.png)

Em relação ao nome que será utilizado nos códigos a *Variable* irá ser "Nome do tipo" + "Variable", com a primeira letra maiúscula. Exemplo se baseando no *enum Direction* criado anteriormente:

```csharp
[SerializeField] private DirectionVariable directionVariable;
```

É possível, também, detectar quando essa *Variable* sofrer alguma alteração apenas atribuindo ao campo *Game Event* um *GameEvent* do mesmo tipo da *Variable*.

![](https://i.imgur.com/K50k9pQl.png)

### Atributos

*Varibles* possuem um atributo capaz de mostrar o valor das mesmas no **Inspector**, para usa-los basta adicionar na frente da *Variable* o atributo **ShowVariableValue**.

```csharp
[ShowVariableValue] [SerializeField]
private IntVariable playerHealth;
```

A representação no **Inspector** pode ser observada a seguir:

![](https://i.imgur.com/hnyEbHZ.png)

O atributo também possui um paramêtro que permite liberar para edição o valor da *Variable* no **Inspector**, para isso basta passar o valor **true** como parâmetro no atributo **ShowVariableValue**:

```csharp
[ShowVariableValue(true)] [SerializeField]
private IntVariable playerHealth;
```

![](https://i.imgur.com/KsEEsR8.png)

# SO-Architecture

## Descrição
Um sistema para arquitetar projetos na Unity baseado em [ScriptableObjects](https://docs.unity3d.com/Manual/class-ScriptableObject.html).

## Features

### SO Generator

Essa janela pode criar *GameEvents*, *Variables* e *Collections* do tipo especificado, no diretório desejado.

![](https://i.imgur.com/Wg0Dv7O.png)

### Atributos

*Varibles* possuem um atributo capaz de mostrar o valor das mesmas no **Inspector**, para usa-los basta adicionar na frente da *Variable* o atributo **ShowVariableValue**.

```csharp
[ShowVariableValue] [SerializeField]
private IntVariable playerHealth;
```

A representação no **Inspector** pode ser observada a seguir:

![](https://i.imgur.com/hnyEbHZ.png)

O atributo também possui um paramêtro que permite liberar para edição o valor da *Variable* no **Inspector**, para isso basta passar o valor **true** como parâmetro no atributo **ShowVariableValue**, demontrado a seguir:

```csharp
[ShowVariableValue(true)] [SerializeField]
private IntVariable playerHealth;
```

Agora, no **Inspector** o valor da *Variable* já pode ser modificado

![](https://i.imgur.com/KsEEsR8.png)

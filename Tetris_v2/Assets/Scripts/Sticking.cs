using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public static class Sticking 
{
    private const float delayHolding = 0.4f;//время задержки при удержании
    private const float period = 0.2f;//период вызова метода

    public static IEnumerator StickingKey(KeyCode keyCode, UnityAction action)
    {
        action();//метод исполняемый по клавише
        float delayTimer = 0;//таймер задержки
        float timer = 0;//таймер нажатичя

        while (true)
        {
            if (!Input.GetKey(keyCode))//проверка зажата ли клавиша
            {
                yield break;//если не зажата выход
            }
            delayTimer += Time.deltaTime;

            if(delayTimer> delayHolding)
            {
                while (true)
                {
                    if (!Input.GetKey(keyCode))//проверка зажата ли клавиша
                    {
                        yield break;//если не зажата выход
                    }
                    timer += Time.deltaTime;

                    if (timer > period)
                    {
                        action();
                        timer = 0;
                    }
                    yield return null;
                }
            }
        }
    }
}

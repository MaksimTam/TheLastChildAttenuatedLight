using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LeverCeller : MonoBehaviour
{
    public GameObject animatedObject; // Ссылка на объект, на котором проигрывается анимация

    private Animator animator; // Ссылка на компонент Animator анимированного объекта

    public GameObject mex00; // Ссылка на объект, на котором проигрывается анимация

    private Animator _animator00; // Ссылка на компонент Animator анимированного объекта

    public GameObject mex01; // Ссылка на объект, на котором проигрывается анимация

    private Animator _animator01; // Ссылка на компонент Animator анимированного объекта

    public GameObject Effect;

    public AudioSource MexDoor;
    public AudioSource Mex;

    public bool _true;

    public GameObject Lever;

    private void Start()
    {
        // Получаем ссылку на компонент Animator
        animator = animatedObject.GetComponent<Animator>();
        _animator00 = mex00.GetComponent<Animator>();
        _animator01 = mex01.GetComponent<Animator>();

        if (_true == true)
        {
            animator.enabled = true;
            _animator00.enabled = true;
            _animator01.enabled = true;
            MexDoor.Stop();
            Mex.Stop();
        }
        else
        {
            Effect.SetActive(false);
            animator.enabled = false;
            MexDoor.enabled = true;
            Mex.enabled = true;
            _animator00.enabled = false;
            _animator01.enabled = false;
        }
    }

    private void Update()
    {

        // Проверяем, нажата ли кнопка мыши на текущем кадре
        if (Input.GetMouseButtonDown(0))
        {
            // Создаем луч, чтобы проверить, что курсор мыши наведен на объект
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Проверяем, есть ли пересечение луча с коллайдером объекта
            if (Physics.Raycast(ray, out hit))
            {
                // Проверяем, является ли объект, на который наведен курсор, нашим объектом
                if (hit.transform.gameObject == gameObject)
                {

                    // Запускаем анимацию на объекте
                    _true = true;
                    if(_true == true)
                    {

                        animator.enabled = true;
                        _animator00.enabled = true;
                        _animator01.enabled = true;
                        Effect.SetActive(true);
                        animator.Play("Anim");
                        MexDoor.Play();
                        Mex.Play();
                        Lever.SetActive(false);
                    }
                   
                    
                }
            }
        }
    }
}


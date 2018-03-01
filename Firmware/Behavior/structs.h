#ifndef _STRUCTS_H_
#define _STRUCTS_H

typedef struct
{
    uint16_t poke0_led, poke1_led, poke2_led;
    uint16_t poke0_valve, poke1_valve, poke2_valve;
    uint16_t led0, led1;
    uint16_t rgb0, rgb1;
    uint16_t do0, do1, do2, do3;
} countdown_t;

typedef struct
{
    uint8_t prescaler_do0, prescaler_do1, prescaler_do2, prescaler_do3;
    uint16_t target_do0, target_do1, target_do2, target_do3;
    uint16_t dcycle_do0, dcycle_do1, dcycle_do2, dcycle_do3;
} timer_conf_t;

typedef struct
{
    bool pwm_do0, pwm_do1, pwm_do2, pwm_do3;
} is_new_timer_conf_t;


/* State of output ports */
typedef struct
{
    bool do0, do1, do2, do3;
} pwm_possibilities_t;

typedef struct
{
    bool do0, do1, do2, do3;
} camera_possibilities_t;

typedef struct
{
    bool do0, do1, do2, do3;
} motor_possibilities_t;

typedef struct
{
    bool port0, port1, port2;
} quad_counter_possibilities_t;


typedef struct
{
    pwm_possibilities_t pwm;
    camera_possibilities_t camera;
    motor_possibilities_t servo;
    quad_counter_possibilities_t quad_counter;
} ports_state_t;

#endif /* _STRUCT_H_ */
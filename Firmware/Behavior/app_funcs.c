#include "app_funcs.h"
#include "app_ios_and_regs.h"
#include "hwbp_core.h"


/************************************************************************/
/* Create pointers to functions                                         */
/************************************************************************/
extern AppRegs app_regs;

void (*app_func_rd_pointer[])(void) = {
	&app_read_REG_POKE_IN,
	&app_read_REG_POKE_DIG_IN,
	&app_read_REG_OUTPUTS_SET,
	&app_read_REG_OUTPUTS_CLEAR,
	&app_read_REG_OUTPUTS_TOGGLE,
	&app_read_REG_OUTPUTS_OUT,
	&app_read_REG_POKE_DIOS_SET,
	&app_read_REG_POKE_DIOS_CLEAR,
	&app_read_REG_POKE_DIOS_TOGGLE,
	&app_read_REG_POKE_DIOS_OUT,
	&app_read_REG_POKE_DIOS_CONF,
	&app_read_REG_POKE_DIOS_IN,
	&app_read_REG_ADC,
	&app_read_REG_MODE_POKE0_LED,
	&app_read_REG_MODE_POKE1_LED,
	&app_read_REG_MODE_POKE2_LED,
	&app_read_REG_MODE_POKE0_VALVE,
	&app_read_REG_MODE_POKE1_VALVE,
	&app_read_REG_MODE_POKE2_VALVE,
	&app_read_REG_MODE_LED0,
	&app_read_REG_MODE_LED1,
	&app_read_REG_MODE_RGB0,
	&app_read_REG_MODE_RGB1,
	&app_read_REG_MODE_DO0,
	&app_read_REG_MODE_DO1,
	&app_read_REG_MODE_DO2,
	&app_read_REG_MODE_DO3,
	&app_read_REG_PULSE_POKE0_LED,
	&app_read_REG_PULSE_POKE1_LED,
	&app_read_REG_PULSE_POKE2_LED,
	&app_read_REG_PULSE_POKE0_VALVE,
	&app_read_REG_PULSE_POKE1_VALVE,
	&app_read_REG_PULSE_POKE2_VALVE,
	&app_read_REG_PULSE_LED0,
	&app_read_REG_PULSE_LED1,
	&app_read_REG_PULSE_RGB0,
	&app_read_REG_PULSE_RGB1,
	&app_read_REG_PULSE_DO0,
	&app_read_REG_PULSE_DO1,
	&app_read_REG_PULSE_DO2,
	&app_read_REG_PULSE_DO3,
	&app_read_REG_FREQ_DO0,
	&app_read_REG_FREQ_DO1,
	&app_read_REG_FREQ_DO2,
	&app_read_REG_FREQ_DO3,
	&app_read_REG_DCYCLE_DO0,
	&app_read_REG_DCYCLE_DO1,
	&app_read_REG_DCYCLE_DO2,
	&app_read_REG_DCYCLE_DO3,
	&app_read_REG_PWM_START,
	&app_read_REG_PWM_STOP,
	&app_read_REG_RGBS,
	&app_read_REG_RGB0,
	&app_read_REG_RGB1,
	&app_read_REG_LED0_CURRENT,
	&app_read_REG_LED1_CURRENT,
	&app_read_REG_LED0_MAX_CURRENT,
	&app_read_REG_LED1_MAX_CURRENT,
	&app_read_REG_EVNT_ENABLE
};

bool (*app_func_wr_pointer[])(void*) = {
	&app_write_REG_POKE_IN,
	&app_write_REG_POKE_DIG_IN,
	&app_write_REG_OUTPUTS_SET,
	&app_write_REG_OUTPUTS_CLEAR,
	&app_write_REG_OUTPUTS_TOGGLE,
	&app_write_REG_OUTPUTS_OUT,
	&app_write_REG_POKE_DIOS_SET,
	&app_write_REG_POKE_DIOS_CLEAR,
	&app_write_REG_POKE_DIOS_TOGGLE,
	&app_write_REG_POKE_DIOS_OUT,
	&app_write_REG_POKE_DIOS_CONF,
	&app_write_REG_POKE_DIOS_IN,
	&app_write_REG_ADC,
	&app_write_REG_MODE_POKE0_LED,
	&app_write_REG_MODE_POKE1_LED,
	&app_write_REG_MODE_POKE2_LED,
	&app_write_REG_MODE_POKE0_VALVE,
	&app_write_REG_MODE_POKE1_VALVE,
	&app_write_REG_MODE_POKE2_VALVE,
	&app_write_REG_MODE_LED0,
	&app_write_REG_MODE_LED1,
	&app_write_REG_MODE_RGB0,
	&app_write_REG_MODE_RGB1,
	&app_write_REG_MODE_DO0,
	&app_write_REG_MODE_DO1,
	&app_write_REG_MODE_DO2,
	&app_write_REG_MODE_DO3,
	&app_write_REG_PULSE_POKE0_LED,
	&app_write_REG_PULSE_POKE1_LED,
	&app_write_REG_PULSE_POKE2_LED,
	&app_write_REG_PULSE_POKE0_VALVE,
	&app_write_REG_PULSE_POKE1_VALVE,
	&app_write_REG_PULSE_POKE2_VALVE,
	&app_write_REG_PULSE_LED0,
	&app_write_REG_PULSE_LED1,
	&app_write_REG_PULSE_RGB0,
	&app_write_REG_PULSE_RGB1,
	&app_write_REG_PULSE_DO0,
	&app_write_REG_PULSE_DO1,
	&app_write_REG_PULSE_DO2,
	&app_write_REG_PULSE_DO3,
	&app_write_REG_FREQ_DO0,
	&app_write_REG_FREQ_DO1,
	&app_write_REG_FREQ_DO2,
	&app_write_REG_FREQ_DO3,
	&app_write_REG_DCYCLE_DO0,
	&app_write_REG_DCYCLE_DO1,
	&app_write_REG_DCYCLE_DO2,
	&app_write_REG_DCYCLE_DO3,
	&app_write_REG_PWM_START,
	&app_write_REG_PWM_STOP,
	&app_write_REG_RGBS,
	&app_write_REG_RGB0,
	&app_write_REG_RGB1,
	&app_write_REG_LED0_CURRENT,
	&app_write_REG_LED1_CURRENT,
	&app_write_REG_LED0_MAX_CURRENT,
	&app_write_REG_LED1_MAX_CURRENT,
	&app_write_REG_EVNT_ENABLE
};


/************************************************************************/
/* REG_POKE_IN                                                          */
/************************************************************************/
void app_read_REG_POKE_IN(void)
{
	//app_regs.REG_POKE_IN = 0;

}

bool app_write_REG_POKE_IN(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_POKE_IN = reg;
	return true;
}


/************************************************************************/
/* REG_POKE_DIG_IN                                                      */
/************************************************************************/
void app_read_REG_POKE_DIG_IN(void)
{
	//app_regs.REG_POKE_DIG_IN = 0;

}

bool app_write_REG_POKE_DIG_IN(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_POKE_DIG_IN = reg;
	return true;
}


/************************************************************************/
/* REG_OUTPUTS_SET                                                      */
/************************************************************************/
void app_read_REG_OUTPUTS_SET(void) {}
bool app_write_REG_OUTPUTS_SET(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	if (reg & B_POKE0_LED) set_POKE0_LED;
	if (reg & B_POKE1_LED) set_POKE1_LED;
	if (reg & B_POKE2_LED) set_POKE2_LED;
	
	if (reg & B_POKE0_VALVE) set_POKE0_VALVE;
	if (reg & B_POKE1_VALVE) set_POKE1_VALVE;
	if (reg & B_POKE2_VALVE) set_POKE2_VALVE;
	
	if (reg & B_LED0) set_LED0;
	if (reg & B_LED1) set_LED1;
	
	if (reg & B_RGB0) rgb0_on = true;
	if (reg & B_RGB1) rgb1_on = true;
	
	if (reg & B_DO0) set_DO0;
	if (reg & B_DO1) set_DO1;
	if (reg & B_DO2) set_DO2;
	if (reg & B_DO3) set_DO3;
        
	return true;
}


/************************************************************************/
/* REG_OUTPUTS_CLEAR                                                    */
/************************************************************************/
void app_read_REG_OUTPUTS_CLEAR(void) {}
bool app_write_REG_OUTPUTS_CLEAR(void *a)
{
	uint16_t reg = *((uint16_t*)a);

    if (reg & B_POKE0_LED) clr_POKE0_LED;
    if (reg & B_POKE1_LED) clr_POKE1_LED;
    if (reg & B_POKE2_LED) clr_POKE2_LED;
    
    if (reg & B_POKE0_VALVE) clr_POKE0_VALVE;
    if (reg & B_POKE1_VALVE) clr_POKE1_VALVE;
    if (reg & B_POKE2_VALVE) clr_POKE2_VALVE;
    
    if (reg & B_LED0) clr_LED0;
    if (reg & B_LED1) clr_LED1;
    
    if (reg & B_RGB0) rgb0_on = false;
    if (reg & B_RGB1) rgb1_on = false;
    
    if (reg & B_DO0) clr_DO0;
    if (reg & B_DO1) clr_DO1;
    if (reg & B_DO2) clr_DO2;
    if (reg & B_DO3) clr_DO3;
    
	return true;
}


/************************************************************************/
/* REG_OUTPUTS_TOGGLE                                                   */
/************************************************************************/
void app_read_REG_OUTPUTS_TOGGLE(void) {}

bool app_write_REG_OUTPUTS_TOGGLE(void *a)
{
	uint16_t reg = *((uint16_t*)a);

    if (reg & B_POKE0_LED) tgl_POKE0_LED;
    if (reg & B_POKE1_LED) tgl_POKE1_LED;
    if (reg & B_POKE2_LED) tgl_POKE2_LED;
    
    if (reg & B_POKE0_VALVE) tgl_POKE0_VALVE;
    if (reg & B_POKE1_VALVE) tgl_POKE1_VALVE;
    if (reg & B_POKE2_VALVE) tgl_POKE2_VALVE;
    
    if (reg & B_LED0) tgl_LED0;
    if (reg & B_LED1) tgl_LED1;
    
    if (reg & B_RGB0)
    {
        if (rgb0_on)
            rgb0_on = false;
        else
            rgb0_on = true;
    }
    if (reg & B_RGB1)
    {
        if (rgb1_on)
            rgb1_on = false;
        else
            rgb1_on = true;
    }
    
    if (reg & B_DO0) tgl_DO0;
    if (reg & B_DO1) tgl_DO1;
    if (reg & B_DO2) tgl_DO2;
    if (reg & B_DO3) tgl_DO3;
    
	return true;
}


/************************************************************************/
/* REG_OUTPUTS_OUT                                                      */
/************************************************************************/
bool rgb0_on, rgb1_on;

void app_read_REG_OUTPUTS_OUT(void)
{
	app_regs.REG_OUTPUTS_OUT = (read_POKE0_LED) ? B_POKE0_LED : 0;
	app_regs.REG_OUTPUTS_OUT |= (read_POKE1_LED) ? B_POKE1_LED : 0;
	app_regs.REG_OUTPUTS_OUT |= (read_POKE2_LED) ? B_POKE2_LED : 0;
    
 	app_regs.REG_OUTPUTS_OUT |= (read_POKE0_VALVE) ? B_POKE0_VALVE : 0;
 	app_regs.REG_OUTPUTS_OUT |= (read_POKE1_VALVE) ? B_POKE1_VALVE : 0;
 	app_regs.REG_OUTPUTS_OUT |= (read_POKE2_VALVE) ? B_POKE2_VALVE : 0;
     
    app_regs.REG_OUTPUTS_OUT |= (read_LED0) ? B_LED0 : 0;
    app_regs.REG_OUTPUTS_OUT |= (read_LED1) ? B_LED1 : 0;
    
    app_regs.REG_OUTPUTS_OUT |= (rgb0_on) ? B_RGB0 : 0;
    app_regs.REG_OUTPUTS_OUT |= (rgb1_on) ? B_RGB1 : 0;
     
    app_regs.REG_OUTPUTS_OUT |= (read_DO0) ? B_DO0 : 0;
    app_regs.REG_OUTPUTS_OUT |= (read_DO1) ? B_DO1 : 0;
    app_regs.REG_OUTPUTS_OUT |= (read_DO2) ? B_DO2 : 0;
    app_regs.REG_OUTPUTS_OUT |= (read_DO3) ? B_DO3 : 0;
}

bool app_write_REG_OUTPUTS_OUT(void *a)
{
	uint16_t reg = *((uint16_t*)a);
    
    if (reg & B_POKE0_LED) set_POKE0_LED; else clr_POKE0_LED;
    if (reg & B_POKE1_LED) set_POKE1_LED; else clr_POKE1_LED;
    if (reg & B_POKE2_LED) set_POKE2_LED; else clr_POKE2_LED;
    
    if (reg & B_POKE0_VALVE) set_POKE0_VALVE; else clr_POKE0_VALVE;
    if (reg & B_POKE1_VALVE) set_POKE1_VALVE; else clr_POKE1_VALVE;
    if (reg & B_POKE2_VALVE) set_POKE2_VALVE; else clr_POKE2_VALVE;
    
    if (reg & B_LED0) set_LED0; else clr_LED0;
    if (reg & B_LED1) set_LED1; else clr_LED1;
    
    if (reg & B_RGB0) rgb0_on = true; else rgb0_on = false;
    if (reg & B_RGB1) rgb1_on = true; else rgb1_on = false;
    
    if (reg & B_DO0) set_DO0; else clr_DO0;
    if (reg & B_DO1) set_DO1; else clr_DO1;
    if (reg & B_DO2) set_DO2; else clr_DO2;
    if (reg & B_DO3) set_DO3; else clr_DO3;    

	app_regs.REG_OUTPUTS_OUT = reg;
	return true;
}


/************************************************************************/
/* REG_POKE_DIOS_SET                                                    */
/************************************************************************/
void app_read_REG_POKE_DIOS_SET(void)
{
	//app_regs.REG_POKE_DIOS_SET = 0;

}

bool app_write_REG_POKE_DIOS_SET(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_POKE_DIOS_SET = reg;
	return true;
}


/************************************************************************/
/* REG_POKE_DIOS_CLEAR                                                  */
/************************************************************************/
void app_read_REG_POKE_DIOS_CLEAR(void)
{
	//app_regs.REG_POKE_DIOS_CLEAR = 0;

}

bool app_write_REG_POKE_DIOS_CLEAR(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_POKE_DIOS_CLEAR = reg;
	return true;
}


/************************************************************************/
/* REG_POKE_DIOS_TOGGLE                                                 */
/************************************************************************/
void app_read_REG_POKE_DIOS_TOGGLE(void)
{
	//app_regs.REG_POKE_DIOS_TOGGLE = 0;

}

bool app_write_REG_POKE_DIOS_TOGGLE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_POKE_DIOS_TOGGLE = reg;
	return true;
}


/************************************************************************/
/* REG_POKE_DIOS_OUT                                                    */
/************************************************************************/
void app_read_REG_POKE_DIOS_OUT(void)
{
	//app_regs.REG_POKE_DIOS_OUT = 0;

}

bool app_write_REG_POKE_DIOS_OUT(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_POKE_DIOS_OUT = reg;
	return true;
}


/************************************************************************/
/* REG_POKE_DIOS_CONF                                                   */
/************************************************************************/
void app_read_REG_POKE_DIOS_CONF(void)
{
	//app_regs.REG_POKE_DIOS_CONF = 0;

}

bool app_write_REG_POKE_DIOS_CONF(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_POKE_DIOS_CONF = reg;
	return true;
}


/************************************************************************/
/* REG_POKE_DIOS_IN                                                     */
/************************************************************************/
void app_read_REG_POKE_DIOS_IN(void)
{
	//app_regs.REG_POKE_DIOS_IN = 0;

}

bool app_write_REG_POKE_DIOS_IN(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_POKE_DIOS_IN = reg;
	return true;
}


/************************************************************************/
/* REG_ADC                                                              */
/************************************************************************/
void app_read_REG_ADC(void)
{
	//app_regs.REG_ADC = 0;

}

bool app_write_REG_ADC(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_ADC = reg;
	return true;
}


/************************************************************************/
/* REG_MODE_POKE0_LED                                                   */
/************************************************************************/
void app_read_REG_MODE_POKE0_LED(void)
{
	//app_regs.REG_MODE_POKE0_LED = 0;

}

bool app_write_REG_MODE_POKE0_LED(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MODE_POKE0_LED = reg;
	return true;
}


/************************************************************************/
/* REG_MODE_POKE1_LED                                                   */
/************************************************************************/
void app_read_REG_MODE_POKE1_LED(void)
{
	//app_regs.REG_MODE_POKE1_LED = 0;

}

bool app_write_REG_MODE_POKE1_LED(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MODE_POKE1_LED = reg;
	return true;
}


/************************************************************************/
/* REG_MODE_POKE2_LED                                                   */
/************************************************************************/
void app_read_REG_MODE_POKE2_LED(void)
{
	//app_regs.REG_MODE_POKE2_LED = 0;

}

bool app_write_REG_MODE_POKE2_LED(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MODE_POKE2_LED = reg;
	return true;
}


/************************************************************************/
/* REG_MODE_POKE0_VALVE                                                 */
/************************************************************************/
void app_read_REG_MODE_POKE0_VALVE(void)
{
	//app_regs.REG_MODE_POKE0_VALVE = 0;

}

bool app_write_REG_MODE_POKE0_VALVE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MODE_POKE0_VALVE = reg;
	return true;
}


/************************************************************************/
/* REG_MODE_POKE1_VALVE                                                 */
/************************************************************************/
void app_read_REG_MODE_POKE1_VALVE(void)
{
	//app_regs.REG_MODE_POKE1_VALVE = 0;

}

bool app_write_REG_MODE_POKE1_VALVE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MODE_POKE1_VALVE = reg;
	return true;
}


/************************************************************************/
/* REG_MODE_POKE2_VALVE                                                 */
/************************************************************************/
void app_read_REG_MODE_POKE2_VALVE(void)
{
	//app_regs.REG_MODE_POKE2_VALVE = 0;

}

bool app_write_REG_MODE_POKE2_VALVE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MODE_POKE2_VALVE = reg;
	return true;
}


/************************************************************************/
/* REG_MODE_LED0                                                        */
/************************************************************************/
void app_read_REG_MODE_LED0(void)
{
	//app_regs.REG_MODE_LED0 = 0;

}

bool app_write_REG_MODE_LED0(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MODE_LED0 = reg;
	return true;
}


/************************************************************************/
/* REG_MODE_LED1                                                        */
/************************************************************************/
void app_read_REG_MODE_LED1(void)
{
	//app_regs.REG_MODE_LED1 = 0;

}

bool app_write_REG_MODE_LED1(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MODE_LED1 = reg;
	return true;
}


/************************************************************************/
/* REG_MODE_RGB0                                                        */
/************************************************************************/
void app_read_REG_MODE_RGB0(void)
{
	//app_regs.REG_MODE_RGB0 = 0;

}

bool app_write_REG_MODE_RGB0(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MODE_RGB0 = reg;
	return true;
}


/************************************************************************/
/* REG_MODE_RGB1                                                        */
/************************************************************************/
void app_read_REG_MODE_RGB1(void)
{
	//app_regs.REG_MODE_RGB1 = 0;

}

bool app_write_REG_MODE_RGB1(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MODE_RGB1 = reg;
	return true;
}


/************************************************************************/
/* REG_MODE_DO0                                                         */
/************************************************************************/
void app_read_REG_MODE_DO0(void)
{
	//app_regs.REG_MODE_DO0 = 0;

}

bool app_write_REG_MODE_DO0(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MODE_DO0 = reg;
	return true;
}


/************************************************************************/
/* REG_MODE_DO1                                                         */
/************************************************************************/
void app_read_REG_MODE_DO1(void)
{
	//app_regs.REG_MODE_DO1 = 0;

}

bool app_write_REG_MODE_DO1(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MODE_DO1 = reg;
	return true;
}


/************************************************************************/
/* REG_MODE_DO2                                                         */
/************************************************************************/
void app_read_REG_MODE_DO2(void)
{
	//app_regs.REG_MODE_DO2 = 0;

}

bool app_write_REG_MODE_DO2(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MODE_DO2 = reg;
	return true;
}


/************************************************************************/
/* REG_MODE_DO3                                                         */
/************************************************************************/
void app_read_REG_MODE_DO3(void)
{
	//app_regs.REG_MODE_DO3 = 0;

}

bool app_write_REG_MODE_DO3(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_MODE_DO3 = reg;
	return true;
}


/************************************************************************/
/* REG_PULSE_POKE0_LED                                                  */
/************************************************************************/
void app_read_REG_PULSE_POKE0_LED(void)
{
	//app_regs.REG_PULSE_POKE0_LED = 0;

}

bool app_write_REG_PULSE_POKE0_LED(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_PULSE_POKE0_LED = reg;
	return true;
}


/************************************************************************/
/* REG_PULSE_POKE1_LED                                                  */
/************************************************************************/
void app_read_REG_PULSE_POKE1_LED(void)
{
	//app_regs.REG_PULSE_POKE1_LED = 0;

}

bool app_write_REG_PULSE_POKE1_LED(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_PULSE_POKE1_LED = reg;
	return true;
}


/************************************************************************/
/* REG_PULSE_POKE2_LED                                                  */
/************************************************************************/
void app_read_REG_PULSE_POKE2_LED(void)
{
	//app_regs.REG_PULSE_POKE2_LED = 0;

}

bool app_write_REG_PULSE_POKE2_LED(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_PULSE_POKE2_LED = reg;
	return true;
}


/************************************************************************/
/* REG_PULSE_POKE0_VALVE                                                */
/************************************************************************/
void app_read_REG_PULSE_POKE0_VALVE(void)
{
	//app_regs.REG_PULSE_POKE0_VALVE = 0;

}

bool app_write_REG_PULSE_POKE0_VALVE(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_PULSE_POKE0_VALVE = reg;
	return true;
}


/************************************************************************/
/* REG_PULSE_POKE1_VALVE                                                */
/************************************************************************/
void app_read_REG_PULSE_POKE1_VALVE(void)
{
	//app_regs.REG_PULSE_POKE1_VALVE = 0;

}

bool app_write_REG_PULSE_POKE1_VALVE(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_PULSE_POKE1_VALVE = reg;
	return true;
}


/************************************************************************/
/* REG_PULSE_POKE2_VALVE                                                */
/************************************************************************/
void app_read_REG_PULSE_POKE2_VALVE(void)
{
	//app_regs.REG_PULSE_POKE2_VALVE = 0;

}

bool app_write_REG_PULSE_POKE2_VALVE(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_PULSE_POKE2_VALVE = reg;
	return true;
}


/************************************************************************/
/* REG_PULSE_LED0                                                       */
/************************************************************************/
void app_read_REG_PULSE_LED0(void)
{
	//app_regs.REG_PULSE_LED0 = 0;

}

bool app_write_REG_PULSE_LED0(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_PULSE_LED0 = reg;
	return true;
}


/************************************************************************/
/* REG_PULSE_LED1                                                       */
/************************************************************************/
void app_read_REG_PULSE_LED1(void)
{
	//app_regs.REG_PULSE_LED1 = 0;

}

bool app_write_REG_PULSE_LED1(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_PULSE_LED1 = reg;
	return true;
}


/************************************************************************/
/* REG_PULSE_RGB0                                                       */
/************************************************************************/
void app_read_REG_PULSE_RGB0(void)
{
	//app_regs.REG_PULSE_RGB0 = 0;

}

bool app_write_REG_PULSE_RGB0(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_PULSE_RGB0 = reg;
	return true;
}


/************************************************************************/
/* REG_PULSE_RGB1                                                       */
/************************************************************************/
void app_read_REG_PULSE_RGB1(void)
{
	//app_regs.REG_PULSE_RGB1 = 0;

}

bool app_write_REG_PULSE_RGB1(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_PULSE_RGB1 = reg;
	return true;
}


/************************************************************************/
/* REG_PULSE_DO0                                                        */
/************************************************************************/
void app_read_REG_PULSE_DO0(void)
{
	//app_regs.REG_PULSE_DO0 = 0;

}

bool app_write_REG_PULSE_DO0(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_PULSE_DO0 = reg;
	return true;
}


/************************************************************************/
/* REG_PULSE_DO1                                                        */
/************************************************************************/
void app_read_REG_PULSE_DO1(void)
{
	//app_regs.REG_PULSE_DO1 = 0;

}

bool app_write_REG_PULSE_DO1(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_PULSE_DO1 = reg;
	return true;
}


/************************************************************************/
/* REG_PULSE_DO2                                                        */
/************************************************************************/
void app_read_REG_PULSE_DO2(void)
{
	//app_regs.REG_PULSE_DO2 = 0;

}

bool app_write_REG_PULSE_DO2(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_PULSE_DO2 = reg;
	return true;
}


/************************************************************************/
/* REG_PULSE_DO3                                                        */
/************************************************************************/
void app_read_REG_PULSE_DO3(void)
{
	//app_regs.REG_PULSE_DO3 = 0;

}

bool app_write_REG_PULSE_DO3(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_PULSE_DO3 = reg;
	return true;
}


/************************************************************************/
/* REG_FREQ_DO0                                                         */
/************************************************************************/
void app_read_REG_FREQ_DO0(void)
{
	//app_regs.REG_FREQ_DO0 = 0;

}

bool app_write_REG_FREQ_DO0(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_FREQ_DO0 = reg;
	return true;
}


/************************************************************************/
/* REG_FREQ_DO1                                                         */
/************************************************************************/
void app_read_REG_FREQ_DO1(void)
{
	//app_regs.REG_FREQ_DO1 = 0;

}

bool app_write_REG_FREQ_DO1(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_FREQ_DO1 = reg;
	return true;
}


/************************************************************************/
/* REG_FREQ_DO2                                                         */
/************************************************************************/
void app_read_REG_FREQ_DO2(void)
{
	//app_regs.REG_FREQ_DO2 = 0;

}

bool app_write_REG_FREQ_DO2(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_FREQ_DO2 = reg;
	return true;
}


/************************************************************************/
/* REG_FREQ_DO3                                                         */
/************************************************************************/
void app_read_REG_FREQ_DO3(void)
{
	//app_regs.REG_FREQ_DO3 = 0;

}

bool app_write_REG_FREQ_DO3(void *a)
{
	uint16_t reg = *((uint16_t*)a);

	app_regs.REG_FREQ_DO3 = reg;
	return true;
}


/************************************************************************/
/* REG_DCYCLE_DO0                                                       */
/************************************************************************/
void app_read_REG_DCYCLE_DO0(void)
{
	//app_regs.REG_DCYCLE_DO0 = 0;

}

bool app_write_REG_DCYCLE_DO0(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DCYCLE_DO0 = reg;
	return true;
}


/************************************************************************/
/* REG_DCYCLE_DO1                                                       */
/************************************************************************/
void app_read_REG_DCYCLE_DO1(void)
{
	//app_regs.REG_DCYCLE_DO1 = 0;

}

bool app_write_REG_DCYCLE_DO1(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DCYCLE_DO1 = reg;
	return true;
}


/************************************************************************/
/* REG_DCYCLE_DO2                                                       */
/************************************************************************/
void app_read_REG_DCYCLE_DO2(void)
{
	//app_regs.REG_DCYCLE_DO2 = 0;

}

bool app_write_REG_DCYCLE_DO2(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DCYCLE_DO2 = reg;
	return true;
}


/************************************************************************/
/* REG_DCYCLE_DO3                                                       */
/************************************************************************/
void app_read_REG_DCYCLE_DO3(void)
{
	//app_regs.REG_DCYCLE_DO3 = 0;

}

bool app_write_REG_DCYCLE_DO3(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_DCYCLE_DO3 = reg;
	return true;
}


/************************************************************************/
/* REG_PWM_START                                                        */
/************************************************************************/
void app_read_REG_PWM_START(void)
{
	//app_regs.REG_PWM_START = 0;

}

bool app_write_REG_PWM_START(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_PWM_START = reg;
	return true;
}


/************************************************************************/
/* REG_PWM_STOP                                                         */
/************************************************************************/
void app_read_REG_PWM_STOP(void)
{
	//app_regs.REG_PWM_STOP = 0;

}

bool app_write_REG_PWM_STOP(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_PWM_STOP = reg;
	return true;
}


/************************************************************************/
/* REG_RGBS                                                             */
/************************************************************************/
// This register is an array with 6 positions
void app_read_REG_RGBS(void)
{
	//app_regs.REG_RGBS[0] = 0;

}

bool app_write_REG_RGBS(void *a)
{
	uint8_t *reg = ((uint8_t*)a);

	app_regs.REG_RGBS[0] = reg[0];
	return true;
}


/************************************************************************/
/* REG_RGB0                                                             */
/************************************************************************/
// This register is an array with 3 positions
void app_read_REG_RGB0(void)
{
	//app_regs.REG_RGB0[0] = 0;

}

bool app_write_REG_RGB0(void *a)
{
	uint8_t *reg = ((uint8_t*)a);

	app_regs.REG_RGB0[0] = reg[0];
	return true;
}


/************************************************************************/
/* REG_RGB1                                                             */
/************************************************************************/
// This register is an array with 3 positions
void app_read_REG_RGB1(void)
{
	//app_regs.REG_RGB1[0] = 0;

}

bool app_write_REG_RGB1(void *a)
{
	uint8_t *reg = ((uint8_t*)a);

	app_regs.REG_RGB1[0] = reg[0];
	return true;
}


/************************************************************************/
/* REG_LED0_CURRENT                                                     */
/************************************************************************/
void app_read_REG_LED0_CURRENT(void)
{
	//app_regs.REG_LED0_CURRENT = 0;

}

bool app_write_REG_LED0_CURRENT(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_LED0_CURRENT = reg;
	return true;
}


/************************************************************************/
/* REG_LED1_CURRENT                                                     */
/************************************************************************/
void app_read_REG_LED1_CURRENT(void)
{
	//app_regs.REG_LED1_CURRENT = 0;

}

bool app_write_REG_LED1_CURRENT(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_LED1_CURRENT = reg;
	return true;
}


/************************************************************************/
/* REG_LED0_MAX_CURRENT                                                 */
/************************************************************************/
void app_read_REG_LED0_MAX_CURRENT(void)
{
	//app_regs.REG_LED0_MAX_CURRENT = 0;

}

bool app_write_REG_LED0_MAX_CURRENT(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_LED0_MAX_CURRENT = reg;
	return true;
}


/************************************************************************/
/* REG_LED1_MAX_CURRENT                                                 */
/************************************************************************/
void app_read_REG_LED1_MAX_CURRENT(void)
{
	//app_regs.REG_LED1_MAX_CURRENT = 0;

}

bool app_write_REG_LED1_MAX_CURRENT(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_LED1_MAX_CURRENT = reg;
	return true;
}


/************************************************************************/
/* REG_EVNT_ENABLE                                                      */
/************************************************************************/
void app_read_REG_EVNT_ENABLE(void)
{
	//app_regs.REG_EVNT_ENABLE = 0;

}

bool app_write_REG_EVNT_ENABLE(void *a)
{
	uint8_t reg = *((uint8_t*)a);

	app_regs.REG_EVNT_ENABLE = reg;
	return true;
}
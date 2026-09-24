#ifndef _MIMIC_H_
#define _MIMIC_H_

#include "app_ios_and_regs.h"

#define _SET_IO_ 0
#define _CLR_IO_ 1
#define _TGL_IO_ 2

void mimic_ir_or_valve (uint8_t reg, uint8_t what_to_do);

#define open_POKE0_VALVE   do { set_POKE0_VALVE;    mimic_ir_or_valve(app_regs.REG_MIMIC_PORT0_VALVE, _SET_IO_); } while(0)
#define close_POKE0_VALVE  do { clr_POKE0_VALVE;    mimic_ir_or_valve(app_regs.REG_MIMIC_PORT0_VALVE, _CLR_IO_); } while(0)
#define toggle_POKE0_VALVE do { tgl_POKE0_VALVE;    mimic_ir_or_valve(app_regs.REG_MIMIC_PORT0_VALVE, _TGL_IO_); } while(0)

#define open_POKE1_VALVE   do { set_POKE1_VALVE;    mimic_ir_or_valve(app_regs.REG_MIMIC_PORT1_VALVE, _SET_IO_); } while(0)
#define close_POKE1_VALVE  do { clr_POKE1_VALVE;    mimic_ir_or_valve(app_regs.REG_MIMIC_PORT1_VALVE, _CLR_IO_); } while(0)
#define toggle_POKE1_VALVE do { tgl_POKE1_VALVE;    mimic_ir_or_valve(app_regs.REG_MIMIC_PORT1_VALVE, _TGL_IO_); } while(0)

#define open_POKE2_VALVE   do { set_POKE2_VALVE;    mimic_ir_or_valve(app_regs.REG_MIMIC_PORT2_VALVE, _SET_IO_); } while(0)
#define close_POKE2_VALVE  do { clr_POKE2_VALVE;    mimic_ir_or_valve(app_regs.REG_MIMIC_PORT2_VALVE, _CLR_IO_); } while(0)
#define toggle_POKE2_VALVE do { tgl_POKE2_VALVE;    mimic_ir_or_valve(app_regs.REG_MIMIC_PORT2_VALVE, _TGL_IO_); } while(0)

#endif /* _MIMIC_H_ */

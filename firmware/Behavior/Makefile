CORE = ATxmega128A1U
MCU = atxmega128a1u
CORE_VERSION = 1.15
PREVIEW_VERSION ?= 0

# Read build metadata from app.h if not defined
APP_H_CONTENTS = $(file < app.h)
finddef = $(filter +%+,$(patsubst #define:$(1):%,+%+,$(subst #define$() $(1)$() ,#define:$(1):,$(strip $(APP_H_CONTENTS)))))
APP_VERSION_NAMES = $(sort $(filter %_VERSION,$(APP_H_CONTENTS)))
APP_VERSION_VALUES = $(subst +,,$(foreach name,$(APP_VERSION_NAMES),$(call finddef,$(name))))
FIRMWARE_VERSION ?= $(word 1,$(APP_VERSION_VALUES)).$(word 3,$(APP_VERSION_VALUES))
HARDWARE_VERSION ?= $(word 2,$(APP_VERSION_VALUES)).$(word 4,$(APP_VERSION_VALUES))
PRJ_NAME = $(subst ",,$(subst +,,$(call finddef,DEVICE_NAME)))

# Version flags
FW_VERSION_WORDS = $(subst ., ,$(firstword $(subst -, ,$(FIRMWARE_VERSION))))
HW_VERSION_WORDS = $(subst ., ,$(firstword $(subst -, ,$(HARDWARE_VERSION))))
MAJOR_FW_VERSION = $(word 1,$(FW_VERSION_WORDS))
MINOR_FW_VERSION = $(word 2,$(FW_VERSION_WORDS))
PATCH_FW_VERSION = $(or $(word 3,$(FW_VERSION_WORDS)), 0)
MAJOR_HW_VERSION = $(word 1,$(HW_VERSION_WORDS))
MINOR_HW_VERSION = $(word 2,$(HW_VERSION_WORDS))
PATCH_HW_VERSION = $(or $(word 3,$(HW_VERSION_WORDS)), 0)
FIRMWARE_FORMAT = $(MAJOR_FW_VERSION).$(MINOR_FW_VERSION)
HARDWARE_FORMAT = $(MAJOR_HW_VERSION).$(MINOR_HW_VERSION)

# Files and directories
BUILD_DIR = bin
PREVIEW_SUFFIX = $(if $(PREVIEW_VERSION:v%=),-preview$(PREVIEW_VERSION),)
BUILD_NAME = $(PRJ_NAME)-fw$(FIRMWARE_FORMAT)-harp$(CORE_VERSION)-hw$(HARDWARE_FORMAT)-ass0$(PREVIEW_SUFFIX)
SRCS = $(wildcard *.c)
OBJS = $(SRCS:%.c=$(BUILD_DIR)/%.o)
LIBS = -l$(CORE)-$(CORE_VERSION)

# Compiler flags
CC = avr-gcc
CFLAGS = \
-x c \
-funsigned-char \
-funsigned-bitfields \
-ffunction-sections \
-fdata-sections \
-fpack-struct \
-fshort-enums \
-mrelax \
-O3 \
-Wall \
-mmcu=$(MCU) \
-DMAJOR_FW_VERSION=$(MAJOR_FW_VERSION) \
-DMINOR_FW_VERSION=$(MINOR_FW_VERSION) \
-DPATCH_FW_VERSION=$(PATCH_FW_VERSION) \
-DMAJOR_HW_VERSION=$(MAJOR_HW_VERSION) \
-DMINOR_HW_VERSION=$(MINOR_HW_VERSION) \
-DPATCH_HW_VERSION=$(PATCH_HW_VERSION) \
-std=gnu99

# Linker flags
LDFLAGS = \
-Wl,--start-group \
-Wl,-lm \
-Wl,$(LIBS) \
-Wl,--end-group \
-Wl,-L"." \
-Wl,--gc-sections \
-mrelax \
-mmcu=$(MCU)

# Object copy flags
OBJCOPY = avr-objcopy
OBJCOPY_FLAGS = \
-O ihex \
-R .eeprom \
-R .fuse \
-R .lock \
-R .signature \
-R .user_signatures \

all: $(BUILD_DIR)/$(BUILD_NAME).hex

clean:
	rm -f $(OBJS) $(OBJS:.o=.d)
	rm -f $(BUILD_DIR)/$(BUILD_NAME).elf
	rm -f $(BUILD_DIR)/$(BUILD_NAME).hex
	rm -f $(BUILD_DIR)/$(BUILD_NAME).map

$(BUILD_DIR):
	mkdir "$(BUILD_DIR)"

$(BUILD_DIR)/%.o: %.c $(BUILD_DIR)
	$(CC) $(CFLAGS) -c $(INCS) -MD -MP -MF "$(@:.o=.d)" -MT"$(@:.o=.d)" -MT"$@" -o $@ $<

$(BUILD_DIR)/%.elf: $(OBJS)
	$(CC) -o $@ $(OBJS) -Wl,-Map="$(@:.elf=.map)" $(LDFLAGS) $(LIBS)

$(BUILD_DIR)/%.hex: $(BUILD_DIR)/%.elf
	$(OBJCOPY) $(OBJCOPY_FLAGS) $< $@

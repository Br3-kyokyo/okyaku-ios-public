class State < ApplicationRecord
  belongs_to  :state_machine
  has_many    :next_transition,  class_name: "Transition", foreign_key: "prev_state_id"
  has_many    :prev_transition,  class_name: "Transition", foreign_key: "next_state_id"
end

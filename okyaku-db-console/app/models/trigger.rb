class Trigger < ApplicationRecord
  belongs_to  :transition
  has_many    :sentences, class_name: "TriggerSentence",  dependent: :destroy
  has_many    :keywords,  class_name: "TriggerKeyword",   dependent: :destroy
  accepts_nested_attributes_for :sentences, allow_destroy: true
  accepts_nested_attributes_for :keywords,  allow_destroy: true

  validates   :transition_id, uniqueness: true
end
